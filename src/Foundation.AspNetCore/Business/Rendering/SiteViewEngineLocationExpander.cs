using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Foundation.AspNetCore.Business.Rendering
{

    public class SiteViewEngineLocationExpander : IViewLocationExpander
    {
        private static readonly string[] AdditionalPartialViewFormats = new[]
            {
                TemplateCoordinator.FoundationPageFolder + "{0}.cshtml",
                TemplateCoordinator.FoundationPageFolder + "{1}/{0}.cshtml",
                TemplateCoordinator.FoundationPageFolder + "{1}/Views/{0}.cshtml",
                TemplateCoordinator.FoundationPageFolder + "%1/{1}/Views/{0}.cshtml",
                TemplateCoordinator.FoundationPageFolder + "%1/Views/Default.cshtml",
                TemplateCoordinator.FoundationBlockFolder + "{0}.cshtml",
                TemplateCoordinator.FoundationBlockFolder + "{1}/{0}.cshtml",
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
                yield return AdditionalPartialViewFormats[i].Replace("%1", GetFeatureName(context.ActionContext.ActionDescriptor));
            }
        }

        private string GetFeatureName(ActionDescriptor descriptor, bool isContainControllerName = false)
        {
            var tokens = descriptor.DisplayName?.Split('.');
            if (!tokens?.Any(t => t == "Features") ?? true)
            {
                return "";
            }

            var controllerName = descriptor.RouteValues.Where(x => x.Key.Equals("controller", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault().Value;
            var selectedTokens = tokens
                .SkipWhile(t => !t.Equals("features",
                    StringComparison.CurrentCultureIgnoreCase))
                .Skip(1)
                .TakeWhile(t => !t.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
            if (isContainControllerName) selectedTokens.Add(controllerName);
            
            var featureName = string.Join("/", selectedTokens);

            return featureName;
        }

        public void PopulateValues(ViewLocationExpanderContext context) { }
    }
}
