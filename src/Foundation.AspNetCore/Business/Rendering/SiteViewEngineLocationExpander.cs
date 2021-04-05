using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Business.Rendering
{

    public class SiteViewEngineLocationExpander : IViewLocationExpander
    {
        private static readonly string[] AdditionalPartialViewFormats = new[]
            {
                TemplateCoordinator.FoundationBlockFolder + "{0}.cshtml",
                TemplateCoordinator.FoundationBlockFolder + "{1}/{0}.cshtml",
                TemplateCoordinator.FoundationPageFolder + "{0}.cshtml",
                TemplateCoordinator.FoundationPageFolder + "{1}/{0}.cshtml",
                TemplateCoordinator.FoundationSharedViewsFolder + "{0}.cshtml",
                TemplateCoordinator.FoundationSharedViewsFolder + "{1}/{0}.cshtml",
                TemplateCoordinator.FoundationHeaderViewsFolder + "{0}.cshtml"
            };

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            foreach (var location in viewLocations)
            {
                yield return location;
            }

            for (int i = 0; i < AdditionalPartialViewFormats.Length; i++)
            {
                yield return AdditionalPartialViewFormats[i];
            }
        }
        public void PopulateValues(ViewLocationExpanderContext context) { }
    }
}
