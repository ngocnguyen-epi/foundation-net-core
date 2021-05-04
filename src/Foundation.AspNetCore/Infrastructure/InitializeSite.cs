using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.CmsPages.Blog.BlogCommentBlock.Interfaces;
using Foundation.AspNetCore.Features.CmsPages.Blog.BlogCommentBlock.Services;
using Foundation.AspNetCore.Features.MyAccount.Bookmarks.Services;
using Foundation.AspNetCore.Features.Search.Services;
using Foundation.AspNetCore.Features.Search.ViewModelFactories;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Services;
using Foundation.AspNetCore.Features.Shared.Commerce.Market.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Market.Services;
using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Features.Shared.Services;
using Foundation.AspNetCore.Infrastructure.Identity;
using Foundation.Cms;
using Mediachase.Commerce;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using System;

namespace Foundation.AspNetCore.Infrastructure
{
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    [ModuleDependency(typeof(InitializationModule))]
    public class InitializeSite : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<IBlogCommentRepository, FakeCommentBlogService>();
            context.Services.AddTransient<IPageCommentRepository, FakePageCommentRepository>();
            context.Services.AddTransient<IPageRepository, PageRepository>();
            context.Services.AddTransient<IUserRepository, UserRepository>();
            context.Services.AddTransient<IBookmarksService, BookmarksService>();
            context.Services.AddSingleton<ISearchViewModelFactory, SearchViewModelFactory>();
            context.Services.AddSingleton<ISearchService, SearchService>();

            context.Services.AddTransient(locator => locator.GetInstance<IOwinContext>().GetUserManager<ApplicationUserManager<SiteUser>>()).AddServiceAccessor<ApplicationUserManager<SiteUser>>();
            context.Services.AddTransient(locator => locator.GetInstance<IOwinContext>().Get<ApplicationSignInManager<SiteUser>>()).AddServiceAccessor<ApplicationSignInManager<SiteUser>>();
            context.Services.AddSingleton<ICurrencyService, CurrencyService>();
            context.Services.AddSingleton<CookieService>();
            context.Services.AddSingleton<ICurrentMarket, CurrentMarket>();
            context.Services.AddSingleton<ICustomerService, CustomerService>();
            context.ConfigurationComplete += (o, e) =>
            {
                e.Services.Intercept<IUpdateCurrentLanguage>(
                (locator, defaultImplementation) =>
                    new LanguageService(
                        locator.GetInstance<ICurrentMarket>(),
                        locator.GetInstance<CookieService>(),
                        defaultImplementation));
            };
        }

        public void Initialize(InitializationEngine context)
        {
            context.InitComplete += ContextOnInitComplete;
        }

        public void Uninitialize(InitializationEngine context)
        {
            context.InitComplete -= ContextOnInitComplete;
        }

        private void ContextOnInitComplete(object sender, EventArgs eventArgs)
        {
            //Extensions.InstallDefaultContent();
            ServiceLocator.Current.GetInstance<ISettingsService>().InitializeSettings();
        }
    }
}
