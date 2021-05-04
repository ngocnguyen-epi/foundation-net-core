using EPiServer.ServiceLocation;
using EPiServer.Web;
using Foundation.AspNetCore.Business;
using Foundation.AspNetCore.Business.Channels;
using Foundation.AspNetCore.Business.Rendering;
using Foundation.AspNetCore.Features.Header;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFoundation(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new SiteViewEngineLocationExpander());
            });

            services.Configure<DisplayOptions>(displayOption =>
            {
                displayOption.Add("full", "/displayoptions/full", Global.ContentAreaTags.FullWidth, "", "epi-icon__layout--full");
                displayOption.Add("half", "/displayoptions/half", Global.ContentAreaTags.HalfWidth, "", "epi-icon__layout--half");
                displayOption.Add("wide", "/displayoptions/wide", Global.ContentAreaTags.TwoThirdsWidth, "", "epi-icon__layout--two-thirds");
                displayOption.Add("narrow", "/displayoptions/narrow", Global.ContentAreaTags.OneThirdWidth, "", "epi-icon__layout--one-third");
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<PageContextActionFilter>();
            });

            services.AddDisplayResolutions();
            services.AddDetection();
            services.AddDependencyInjection();
        }

        private static void AddDisplayResolutions(this IServiceCollection services)
        {
            services.AddSingleton<StandardResolution>();
            services.AddSingleton<IpadHorizontalResolution>();
            services.AddSingleton<IphoneVerticalResolution>();
            services.AddSingleton<AndroidVerticalResolution>();
        }
        private static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IHeaderViewModelFactory, HeaderViewModelFactory>();
        }
    }
}
