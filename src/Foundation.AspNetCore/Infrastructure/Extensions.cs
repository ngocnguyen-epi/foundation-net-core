using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Enterprise;
using EPiServer.Logging;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Extensions;
using Foundation.AspNetCore.Features.CatalogContents.Variation.Models;
using Foundation.AspNetCore.Features.CmsPages.Folder;
using Foundation.AspNetCore.Features.CmsPages.Home;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Foundation.AspNetCore.Infrastructure
{
    public static class Extensions
    {
        private static readonly Lazy<IContentRepository> _contentRepository =
            new Lazy<IContentRepository>(() => ServiceLocator.Current.GetInstance<IContentRepository>());

        private static readonly Lazy<IUrlResolver> _urlResolver =
            new Lazy<IUrlResolver>(() => ServiceLocator.Current.GetInstance<IUrlResolver>());

        private static readonly Lazy<ISiteDefinitionRepository> _siteDefinitionRepository =
            new Lazy<ISiteDefinitionRepository>(() => ServiceLocator.Current.GetInstance<ISiteDefinitionRepository>());

        private static readonly Lazy<ContentRootService> _contentRootService =
            new Lazy<ContentRootService>(() => ServiceLocator.Current.GetInstance<ContentRootService>());

        public static ContentReference GetRelativeStartPage(this IContent content)
        {
            if (content is HomePage)
            {
                return content.ContentLink;
            }

            var ancestors = _contentRepository.Value.GetAncestors(content.ContentLink);
            var startPage = ancestors.FirstOrDefault(x => x is HomePage) as HomePage;
            return startPage == null ? ContentReference.StartPage : startPage.ContentLink;
        }

        public static IEnumerable<T> FindPagesRecursively<T>(this IContentLoader contentLoader, PageReference pageLink) where T : PageData
        {
            foreach (var child in contentLoader.GetChildren<T>(pageLink))
            {
                yield return child;
            }

            foreach (var folder in contentLoader.GetChildren<FolderPage>(pageLink))
            {
                foreach (var nestedChild in contentLoader.FindPagesRecursively<T>(folder.PageLink))
                {
                    yield return nestedChild;
                }
            }
        }

        public static void InstallDefaultContent()
        {
            //if (_siteDefinitionRepository.Value.List().Any() || Type.GetType("Foundation.AspNetCore.Features.Setup.SetupController, Foundation") != null)
            //{
            //    return;
            //}

            //var siteDefinition = new SiteDefinition
            //{
            //    Name = "foundation-mvc-cms",
            //    SiteUrl = new Uri($"http://{WebHostingEnvironment.Instance.WebRootPath}/"),
            //};

            //siteDefinition.Hosts.Add(new HostDefinition()
            //{
            //    Name = WebHostingEnvironment.Instance.WebRootPath,
            //    Type = HostDefinitionType.Primary
            //});

            //siteDefinition.Hosts.Add(new HostDefinition()
            //{
            //    Name = HostDefinition.WildcardHostName,
            //    Type = HostDefinitionType.Undefined
            //});

            var registeredRoots = _contentRepository.Value.GetItems(_contentRootService.Value.List(), new LoaderOptions());
            var settingsRootRegistered = registeredRoots.Any(x => x.ContentGuid == SettingsFolder.SettingsRootGuid && x.Name.Equals(SettingsFolder.SettingsRootName));

            if (!settingsRootRegistered)
            {
                _contentRootService.Value.Register<SettingsFolder>(SettingsFolder.SettingsRootName + "IMPORT", SettingsFolder.SettingsRootGuid, ContentReference.RootPage);
            }

            //CreateSite(new FileStream(
            //        WebHostingEnvironment.Instance.WebRootPath + "/App_Data/foundation.episerverdata" ?? throw new InvalidOperationException(),
            //        FileMode.Open,
            //        FileAccess.Read,
            //        FileShare.Read),
            //    siteDefinition,
            //    ContentReference.RootPage);

            //RunIndexJob(ServiceLocator.Current.GetInstance<IScheduledJobExecutor>(),
            //        ServiceLocator.Current.GetInstance<IScheduledJobRepository>(), new Guid("8EB257F9-FF22-40EC-9958-C1C5BA8C2A53"));
        }

        private static void RunIndexJob(IScheduledJobExecutor scheduledJobExecutor, IScheduledJobRepository scheduledJobRepository, Guid jobId)
        {
            var job = scheduledJobRepository.Get(jobId);
            if (job == null)
            {
                return;
            }

            scheduledJobExecutor.StartAsync(job, new JobExecutionOptions { Trigger = ScheduledJobTrigger.User });
        }

        private static void CreateSite(Stream stream, SiteDefinition siteDefinition, ContentReference startPage)
        {
            //EPiServer.Find.Cms.EventedIndexingSettings.Instance.EventedIndexingEnabled = false;
            //EPiServer.Find.Cms.EventedIndexingSettings.Instance.ScheduledPageQueueEnabled = false;
            ImportEpiserverContent(stream, startPage, ServiceLocator.Current.GetInstance<IDataImporter>(), siteDefinition);
            //EPiServer.Find.Cms.EventedIndexingSettings.Instance.EventedIndexingEnabled = true;
            //EPiServer.Find.Cms.EventedIndexingSettings.Instance.ScheduledPageQueueEnabled = true;
        }

        public static bool ImportEpiserverContent(Stream stream,
            ContentReference destinationRoot,
            IDataImporter importer,
            SiteDefinition siteDefinition = null)
        {
            var success = false;
            try
            {
                var log = importer.Import(stream, destinationRoot, new ImportOptions
                {
                    KeepIdentity = true,
                    EnsureContentNameUniqueness = false
                });

                var status = importer.Status;

                if (status == null)
                {
                    return false;
                }

                UpdateLanguageBranches(status);
                if (siteDefinition != null && !ContentReference.IsNullOrEmpty(status.ImportedRoot))
                {
                    siteDefinition.StartPage = status.ImportedRoot;
                    _siteDefinitionRepository.Value.Save(siteDefinition);
                    SiteDefinition.Current = siteDefinition;
                    success = true;
                }
            }
            catch (Exception exception)
            {
                LogManager.GetLogger().Error(exception.Message, exception);
                success = false;
            }

            return success;
        }

        private static void UpdateLanguageBranches(IImportStatus status)
        {
            var languageBranchRepository = ServiceLocator.Current.GetInstance<ILanguageBranchRepository>();

            if (status.ContentLanguages == null)
            {
                return;
            }

            foreach (var languageId in status.ContentLanguages)
            {
                var languageBranch = languageBranchRepository.Load(languageId);

                if (languageBranch == null)
                {
                    languageBranch = new LanguageBranch(languageId);
                    languageBranchRepository.Save(languageBranch);
                }
                else if (!languageBranch.Enabled)
                {
                    languageBranch = languageBranch.CreateWritableClone();
                    languageBranch.Enabled = true;
                    languageBranchRepository.Save(languageBranch);
                }
            }
        }

        public static ContactViewModel GetContactViewModelById(this ICustomerService customerService, string id) => new ContactViewModel(customerService.GetContactById(id));

        public static bool IsVirtualVariant(this ILineItem lineItem)
        {
            var entry = lineItem.GetEntryContent<EntryContentBase>() as GenericVariant;
            return entry != null && entry.VirtualProductMode != null && !string.IsNullOrWhiteSpace(entry.VirtualProductMode) && !entry.VirtualProductMode.Equals("None");
        }
    }
}