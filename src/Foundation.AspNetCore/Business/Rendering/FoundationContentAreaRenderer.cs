using EPiServer;
using EPiServer.Core;
using EPiServer.Core.Html.StringParsing;
using EPiServer.Web.Mvc.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Foundation.AspNetCore.Business.Rendering
{
    /// <summary>
    /// Extends the default <see cref="ContentAreaRenderer"/> to apply custom CSS classes to each <see cref="ContentFragment"/>.
    /// </summary>
    public class FoundationContentAreaRenderer : ContentAreaRenderer
    {
        protected override string GetContentAreaItemCssClass(IHtmlHelper htmlHelper, ContentAreaItem contentAreaItem)
        {
            var baseItemClass = base.GetContentAreaItemCssClass(htmlHelper, contentAreaItem);

            var tag = GetContentAreaItemTemplateTag(htmlHelper, contentAreaItem);
            return $"block {baseItemClass} {GetTypeSpecificCssClasses(contentAreaItem, ContentRepository)} {GetCssClassForTag(tag)} {tag}";
        }

        /// <summary>
        /// Gets a CSS class used for styling based on a tag name (ie a Bootstrap class name)
        /// </summary>
        /// <param name="tagName">Any tag name available, see <see cref="Global.ContentAreaTags"/></param>
        private static string GetCssClassForTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                return "col-12";
            }
            switch (tagName.ToLower())
            {
                case "col-12":
                    return "full";
                case "col-8":
                    return "wide";
                case "col-6":
                    return "half";
                default:
                    return string.Empty;
            }
        }

        private static string GetTypeSpecificCssClasses(ContentAreaItem contentAreaItem, IContentRepository contentRepository)
        {
            var content = contentAreaItem.GetContent();
            var cssClass = content == null ? String.Empty : content.GetOriginalType().Name.ToLowerInvariant();

            var customClassContent = content as ICustomCssInContentArea;
            if (customClassContent != null && !string.IsNullOrWhiteSpace(customClassContent.ContentAreaCssClass))
            {
                cssClass += string.Format(" {0}", customClassContent.ContentAreaCssClass);
            }

            return cssClass;
        }
    }
}
