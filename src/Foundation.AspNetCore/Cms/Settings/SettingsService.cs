using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Framework.TypeScanner;
using EPiServer.Logging;
using EPiServer.Security;
using EPiServer.Web;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Cms.Settings
{
    public class SettingsService : ISettingsService
    {
        public const string GlobalSettingsRootName = "Global Settings Root";
        private readonly IContentRepository _contentRepository;
        private readonly ContentRootService _contentRootService;
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly ILogger _log = LogManager.GetLogger();
        private readonly ITypeScannerLookup _typeScannerLookup;
        private readonly IContentEvents _contentEvents;
        private readonly ISiteDefinitionEvents _siteDefinitionEvents;
        private readonly ISiteDefinitionRepository _siteDefinitionRepository;
        private readonly ISiteDefinitionResolver _siteDefinitionResolver;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SettingsService(
            IContentRepository contentRepository,
            ContentRootService contentRootService,
            ITypeScannerLookup typeScannerLookup,
            IContentTypeRepository contentTypeRepository,
            IContentEvents contentEvents,
            ISiteDefinitionEvents siteDefinitionEvents,
            ISiteDefinitionRepository siteDefinitionRepository,
            ISiteDefinitionResolver siteDefinitionResolver,
            IHttpContextAccessor httpContextAccessor)
        {
            _contentRepository = contentRepository;
            _contentRootService = contentRootService;
            _typeScannerLookup = typeScannerLookup;
            _contentTypeRepository = contentTypeRepository;
            _contentEvents = contentEvents;
            _siteDefinitionEvents = siteDefinitionEvents;
            _siteDefinitionRepository = siteDefinitionRepository;
            _siteDefinitionResolver = siteDefinitionResolver;
            _httpContextAccessor = httpContextAccessor;
        }

        public ConcurrentDictionary<Guid, Dictionary<Type, object>> SiteSettings { get; } = new ConcurrentDictionary<Guid, Dictionary<Type, object>>();

        public ContentReference GlobalSettingsRoot { get; set; }

        public T GetSiteSettings<T>(Guid? siteId = null)
        {
            if (!siteId.HasValue)
            {
                siteId = ResolveSiteId();
                if (siteId == Guid.Empty)
                {
                    return default;
                }
            }
            try
            {
                if (SiteSettings.TryGetValue(siteId.Value, out var siteSettings) && siteSettings.TryGetValue(typeof(T), out var setting))
                {
                    return (T)setting;
                }
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                _log.Error($"[Settings] {keyNotFoundException.Message}", exception: keyNotFoundException);
            }
            catch (ArgumentNullException argumentNullException)
            {
                _log.Error($"[Settings] {argumentNullException.Message}", exception: argumentNullException);
            }

            return default;
        }

        public void UpdateSettings(Guid siteId, IContent content)
        {
            var contentType = content.GetOriginalType();
            try
            {
                if (!SiteSettings.ContainsKey(siteId))
                {
                    SiteSettings[siteId] = new Dictionary<Type, object>();
                }

                SiteSettings[siteId][contentType] = content;
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                _log.Error($"[Settings] {keyNotFoundException.Message}", exception: keyNotFoundException);
            }
            catch (ArgumentNullException argumentNullException)
            {
                _log.Error($"[Settings] {argumentNullException.Message}", exception: argumentNullException);
            }
        }

        public void InitializeSettings()
        {
            try
            {
                RegisterContentRoots();
            }
            catch (NotSupportedException notSupportedException)
            {
                _log.Error($"[Settings] {notSupportedException.Message}", exception: notSupportedException);
                throw;
            }

            _contentEvents.PublishedContent += PublishedContent;
            _siteDefinitionEvents.SiteCreated += SiteCreated;
            _siteDefinitionEvents.SiteUpdated += SiteUpdated;
            _siteDefinitionEvents.SiteDeleted += SiteDeleted;
        }

        public void UnintializeSettings()
        {
            _contentEvents.PublishedContent -= PublishedContent;
            _siteDefinitionEvents.SiteCreated -= SiteCreated;
            _siteDefinitionEvents.SiteUpdated -= SiteUpdated;
            _siteDefinitionEvents.SiteDeleted -= SiteDeleted;
        }

        public void UpdateSettings()
        {
            var root = _contentRepository.GetItems(_contentRootService.List(), new LoaderOptions())
                 .FirstOrDefault(x => x.ContentGuid == SettingsFolder.SettingsRootGuid);

            if (root == null)
            {
                return;
            }

            GlobalSettingsRoot = root.ContentLink;
            var children = _contentRepository.GetChildren<SettingsFolder>(GlobalSettingsRoot).ToList();
            foreach (var site in _siteDefinitionRepository.List())
            {
                var folder = children.Find(x => x.Name.Equals(site.Name, StringComparison.InvariantCultureIgnoreCase));
                if (folder != null)
                {
                    foreach (var child in _contentRepository.GetChildren<SettingsBase>(folder.ContentLink))
                    {
                        UpdateSettings(site.Id, child);
                    }
                    continue;
                }
                CreateSiteFolder(site);
            }
        }

        private void RegisterContentRoots()
        {
            var registeredRoots = _contentRepository.GetItems(_contentRootService.List(), new LoaderOptions());
            var settingsRootRegistered = registeredRoots.Any(x => x.ContentGuid == SettingsFolder.SettingsRootGuid && x.Name.Equals(SettingsFolder.SettingsRootName));

            if (!settingsRootRegistered)
            {
                _contentRootService.Register<SettingsFolder>(SettingsFolder.SettingsRootName, SettingsFolder.SettingsRootGuid, ContentReference.RootPage);
            }

            UpdateSettings();
        }

        private void CreateSiteFolder(SiteDefinition siteDefinition)
        {
            var folder = _contentRepository.GetDefault<SettingsFolder>(GlobalSettingsRoot);
            folder.Name = siteDefinition.Name;
            var reference = _contentRepository.Save(folder, SaveAction.Publish, AccessLevel.NoAccess);

            var settingsModelTypes = _typeScannerLookup.AllTypes
                .Where(t => t.GetCustomAttributes(typeof(SettingsContentTypeAttribute), false).Length > 0);

            foreach (var settingsType in settingsModelTypes)
            {
                if (!(settingsType.GetCustomAttributes(typeof(SettingsContentTypeAttribute), false)
                    .FirstOrDefault() is SettingsContentTypeAttribute attribute))
                {
                    continue;
                }

                var contentType = _contentTypeRepository.Load(settingsType);
                var newSettings = _contentRepository.GetDefault<IContent>(reference, contentType.ID);
                newSettings.Name = attribute.SettingsName;
                _contentRepository.Save(newSettings, SaveAction.Publish, AccessLevel.NoAccess);
                UpdateSettings(siteDefinition.Id, newSettings);
            }
        }

        private void SiteCreated(object sender, SiteDefinitionEventArgs e)
        {
            if (_contentRepository.GetChildren<SettingsFolder>(GlobalSettingsRoot)
                .Any(x => x.Name.Equals(e.Site.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                return;
            }

            CreateSiteFolder(e.Site);
        }

        private void SiteDeleted(object sender, SiteDefinitionEventArgs e)
        {
            var folder = _contentRepository.GetChildren<SettingsFolder>(GlobalSettingsRoot)
                .FirstOrDefault(x => x.Name.Equals(e.Site.Name, StringComparison.InvariantCultureIgnoreCase));

            if (folder == null)
            {
                return;
            }

            _contentRepository.Delete(folder.ContentLink, true, AccessLevel.NoAccess);
        }

        private void SiteUpdated(object sender, SiteDefinitionEventArgs e)
        {
            var updatedArgs = e as SiteDefinitionUpdatedEventArgs;
            var prevSite = updatedArgs.PreviousSite;
            var updatedSite = updatedArgs.Site;
            var settingsRoot = GlobalSettingsRoot;
            var currentSettingsFolder = _contentRepository.GetChildren<IContent>(settingsRoot)
                .FirstOrDefault(x => x.Name.Equals(prevSite.Name, StringComparison.InvariantCultureIgnoreCase)) as ContentFolder;
            if (currentSettingsFolder != null)
            {
                var cloneFolder = currentSettingsFolder.CreateWritableClone();
                cloneFolder.Name = updatedSite.Name;
                _contentRepository.Save(cloneFolder);
                return;
            }


            CreateSiteFolder(e.Site);
        }

        private void PublishedContent(object sender, ContentEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            if (e.Content is SettingsBase)
            {
                var parent = _contentRepository.Get<IContent>(e.Content.ParentLink);
                var site = _siteDefinitionRepository.Get(parent.Name);

                var id = site?.Id;
                if (id == null || id == Guid.Empty)
                {
                    return;
                }
                UpdateSettings(id.Value, e.Content);
            }
        }

        private Guid ResolveSiteId()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                return Guid.Empty;
            }

            var site = _siteDefinitionResolver.GetByHostname(request.Host.Value, true);
            if (site == null)
            {
                return Guid.Empty;
            }
            return site.Id;
        }
    }
}