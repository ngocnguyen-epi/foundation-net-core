using EPiServer;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.CmsPages.Home;
using Foundation.AspNetCore.Features.MyOrganization.Shared.Interfaces;
using Foundation.AspNetCore.Features.MyOrganization.Shared.ViewModels;
using Foundation.AspNetCore.Features.Settings;
using Foundation.AspNetCore.Features.Shared.Commerce.Organization.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Foundation.AspNetCore.Features.MyOrganization.Shared.Components
{
    public class B2BNavigationComponent : ViewComponent
    {
        private readonly IContentLoader _contentLoader;
        private readonly IOrganizationService _organizationService;
        private readonly IB2BNavigationService _b2bNavigationService;
        private readonly ISettingsService _settingsService;

        public B2BNavigationComponent(IContentLoader contentLoader,
            IOrganizationService organizationService,
            IB2BNavigationService b2bNavigationService,
            ISettingsService settingsService)
        {
            _contentLoader = contentLoader;
            _organizationService = organizationService;
            _b2bNavigationService = b2bNavigationService;
            _settingsService = settingsService;
        }

        public IViewComponentResult Invoke(IContent currentContent)
        {
            var startPage = _contentLoader.Get<HomePage>(ContentReference.StartPage);
            var layoutSettings = _settingsService.GetSiteSettings<LayoutSettings>();
            var viewModel = new B2BNavigationViewModel
            {
                StartPage = startPage,
                CurrentContentLink = currentContent?.ContentLink,
                CurrentContentGuid = currentContent?.ContentGuid ?? Guid.Empty,
                UserLinks = new LinkItemCollection()
            };

            var organization = _organizationService.GetCurrentFoundationOrganization();
            if (organization == null)
            {
                return View("_B2BNavigation.cshtml", viewModel);
            }

            if (layoutSettings?.OrganizationMenu != null)
            {
                viewModel.UserLinks.AddRange(_b2bNavigationService.FilterB2BNavigationForCurrentUser(layoutSettings.OrganizationMenu));
            }

            return View("_B2BNavigation.cshtml", viewModel);
        }
    }
}