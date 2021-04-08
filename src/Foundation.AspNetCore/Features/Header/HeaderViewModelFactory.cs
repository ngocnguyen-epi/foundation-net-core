﻿using EPiServer;
using EPiServer.Core;
using EPiServer.Data;
using EPiServer.Editor;
using EPiServer.Filters;
using EPiServer.Framework.Localization;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Cms.Extensions;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.Blocks.MenuItemBlock;
using Foundation.AspNetCore.Features.CmsPages.Home;
using Foundation.AspNetCore.Features.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Header
{
    public class HeaderViewModelFactory : IHeaderViewModelFactory
    {
        private const string MenuCacheKey = "MenuItemsCacheKey";
        private readonly LocalizationService _localizationService;
        private readonly IUrlResolver _urlResolver;
        private readonly IContentCacheKeyCreator _contentCacheKeyCreator;
        private readonly IContentLoader _contentLoader;
        private readonly IDatabaseMode _databaseMode;
        private readonly ISettingsService _settingsService;
        private readonly IContentVersionRepository _contentVersionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContextModeResolver _contextModeResolver;

        public HeaderViewModelFactory(LocalizationService localizationService,
            IUrlResolver urlResolver,
            IContentCacheKeyCreator contentCacheKeyCreator,
            IContentLoader contentLoader,
            IDatabaseMode databaseMode,
            ISettingsService settingsService,
            IContentVersionRepository contentVersionRepository,
            IHttpContextAccessor httpContextAccessor,
            IContextModeResolver contextModeResolver)
        {
            _localizationService = localizationService;
            _urlResolver = urlResolver;
            _contentCacheKeyCreator = contentCacheKeyCreator;
            _contentLoader = contentLoader;
            _databaseMode = databaseMode;
            _settingsService = settingsService;
            _contentVersionRepository = contentVersionRepository;
            _httpContextAccessor = httpContextAccessor;
            _contextModeResolver = contextModeResolver;
        }

        public void AddMyAccountMenu(HomePage homePage, HeaderViewModel viewModel)
        {
            if (_httpContextAccessor.HttpContext != null && !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.UserLinks = new LinkItemCollection();
                return;
            }

            var menuItems = new LinkItemCollection();
            var filter = new FilterContentForVisitor();
            var layoutSettings = _settingsService.GetSiteSettings<LayoutSettings>();

            foreach (var linkItem in layoutSettings.MyAccountCmsMenu ?? new LinkItemCollection())
            {
                if (!UrlResolver.Current.TryToPermanent(linkItem.Href, out var linkUrl))
                {
                    continue;
                }

                if (linkUrl.IsNullOrEmpty())
                {
                    continue;
                }

                var urlBuilder = new UrlBuilder(linkUrl);
                var content = _urlResolver.Route(urlBuilder);
                if (content == null || filter.ShouldFilter(content))
                {
                    continue;
                }

                linkItem.Title = linkItem.Text;
                menuItems.Add(linkItem);
            }

            var signoutText = _localizationService.GetString("/Header/Account/SignOut", "Sign Out");
            var link = new LinkItem
            {
                Href = "/publicapi/signout",
                Text = signoutText,
                Title = signoutText
            };
            link.Attributes.Add("css", "fa-sign-out");
            menuItems.Add(link);

            viewModel.UserLinks.AddRange(menuItems);
        }

        public HeaderViewModel CreateHeaderViewModel(IContent content, HomePage homePage)
        {
            var layoutSettings = _settingsService.GetSiteSettings<LayoutSettings>();
            var viewModel = CreateViewModel(content, homePage);

            viewModel.LargeHeaderMenu = layoutSettings?.LargeHeaderMenu ?? true;
            viewModel.LayoutSettings = layoutSettings;
            viewModel.LabelSettings = _settingsService.GetSiteSettings<LabelSettings>();
            viewModel.ReferencePageSettings = _settingsService.GetSiteSettings<ReferencePageSettings>();
            viewModel.SearchSettings = _settingsService.GetSiteSettings<SearchSettings>();
            return viewModel;
        }

        protected virtual HeaderViewModel CreateViewModel(IContent currentContent, HomePage homePage)
        {
            var menuItems = new List<MenuItemViewModel>();
            var homeLanguage = homePage.Language.DisplayName;
            var layoutSettings = _settingsService.GetSiteSettings<LayoutSettings>();
            var filter = new FilterContentForVisitor();
            menuItems = layoutSettings?.MainMenu?.FilteredItems.Where(x =>
            {
                var _menuItem = _contentLoader.Get<IContent>(x.ContentLink);
                MenuItemBlock _menuItemBlock;
                if (_menuItem is MenuItemBlock)
                {
                    _menuItemBlock = _menuItem as MenuItemBlock;
                    if (_menuItemBlock.Link == null)
                    {
                        return true;
                    }
                    var linkedItem = UrlResolver.Current.Route(new UrlBuilder(_menuItemBlock.Link));
                    if (filter.ShouldFilter(linkedItem))
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).Select(x =>
            {
                var content = _contentLoader.Get<IContent>(x.ContentLink);
                MenuItemBlock _;
                MenuItemViewModel menuItem;
                if (content is MenuItemBlock)
                {
                    _ = content as MenuItemBlock;
                    menuItem = new MenuItemViewModel
                    {
                        Name = _.Name,
                        ButtonText = _.ButtonText,
                        //TeaserText = _.TeaserText,
                        Uri = _.Link == null ? string.Empty : _urlResolver.GetUrl(new UrlBuilder(_.Link.ToString()), new UrlResolverArguments() { ContextMode = ContextMode.Default }),
                        ImageUrl = !ContentReference.IsNullOrEmpty(_.MenuImage) ? _urlResolver.GetUrl(_.MenuImage) : "",
                        ButtonLink = _.ButtonLink?.Host + _.ButtonLink?.PathAndQuery,
                        ChildLinks = _.ChildItems?.ToList() ?? new List<GroupLinkCollection>()
                    };
                }
                else
                {
                    menuItem = new MenuItemViewModel
                    {
                        Name = content.Name,
                        Uri = _urlResolver.GetUrl(content.ContentLink),
                        //ChildLinks = new List<GroupLinkCollection>()
                    };
                }

                if (!_contextModeResolver.CurrentMode.EditOrPreview())
                {
                    var keyDependency = new List<string>
                    {
                        _contentCacheKeyCreator.CreateCommonCacheKey(homePage.ContentLink), // If The HomePage updates menu (remove MenuItems)
                        _contentCacheKeyCreator.CreateCommonCacheKey(x.ContentLink)
                    };
                }

                return menuItem;
            }).ToList();

            return new HeaderViewModel
            {
                HomePage = homePage,
                CurrentContentLink = currentContent?.ContentLink,
                CurrentContentGuid = currentContent?.ContentGuid ?? Guid.Empty,
                UserLinks = new LinkItemCollection(),
                IsReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly,
                MenuItems = menuItems ?? new List<MenuItemViewModel>(),
                IsInEditMode = _contextModeResolver.CurrentMode.EditOrPreview()
            };
        }
    }
}
