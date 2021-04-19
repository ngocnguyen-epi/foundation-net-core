using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.CmsPages.Blog.BlogCommentBlock.Interfaces;
using Foundation.AspNetCore.Features.CmsPages.Blog.BlogCommentBlock.Services;
using Foundation.AspNetCore.Features.MyAccount.Bookmarks.Services;
using Foundation.AspNetCore.Features.Search.Services;
using Foundation.AspNetCore.Features.Search.ViewModelFactories;
using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Features.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
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
