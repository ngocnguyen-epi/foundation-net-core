﻿using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;

namespace Foundation.AspNetCore.Cms.Extensions
{
    public static class UrlHelpers
    {
        private static readonly Lazy<IUrlResolver> UrlResolver =
           new Lazy<IUrlResolver>(() => ServiceLocator.Current.GetInstance<IUrlResolver>());

        private static readonly Lazy<IContentLoader> ContentLoader =
            new Lazy<IContentLoader>(() => ServiceLocator.Current.GetInstance<IContentLoader>());

        public static RouteValueDictionary ContentRoute(this IUrlHelper urlHelper,
            ContentReference contentLink,
            object routeValues = null)
        {
            var first = new RouteValueDictionary(routeValues);

            var values = first.Union(urlHelper.ActionContext.RouteData.Values);

            values[RoutingConstants.ActionKey] = "index";
            values[RoutingConstants.NodeKey] = contentLink;
            return values;
        }

        /// <summary>
        ///     Returns the target URL for a PageReference. Respects the page's shortcut setting
        ///     so if the page is set as a shortcut to another page or an external URL that URL
        ///     will be returned.
        /// </summary>
        public static HtmlString PageLinkUrl(this IUrlHelper urlHelper,
            ContentReference pageLink)
        {
            if (ContentReference.IsNullOrEmpty(pageLink))
                return HtmlString.Empty;

            var page = ContentLoader.Value.Get<PageData>(pageLink);

            return PageLinkUrl(urlHelper, page);
        }

        /// <summary>
        ///     Returns the target URL for a page. Respects the page's shortcut setting
        ///     so if the page is set as a shortcut to another page or an external URL that URL
        ///     will be returned.
        /// </summary>
        public static HtmlString PageLinkUrl(this IUrlHelper urlHelper,
            PageData page)
        {
            switch (page.LinkType)
            {
                case PageShortcutType.Normal:
                case PageShortcutType.FetchData:
                    return new HtmlString(UrlResolver.Value.GetUrl(page.PageLink));

                case PageShortcutType.Shortcut:
                    var shortcutProperty = page.Property["PageShortcutLink"] as PropertyPageReference;
                    if (shortcutProperty != null && !ContentReference.IsNullOrEmpty(shortcutProperty.PageLink))
                        return urlHelper.PageLinkUrl(shortcutProperty.PageLink);
                    break;

                case PageShortcutType.External:
                    return new HtmlString(page.LinkURL);
            }

            return HtmlString.Empty;
        }

        public static HtmlString GetSegmentedUrl(this IUrlHelper urlHelper,
            PageData currentPage,
            params string[] segments)
        {
            var url = urlHelper.PageLinkUrl(currentPage).ToString();

            if (!url.EndsWith("/"))
                url = url + '/';
            url += string.Join("/", segments);
            //TODO: Url-encode segments

            return new HtmlString(url);
        }

        public static HtmlString ImageExternalUrl(this IUrlHelper urlHelper,
            ImageData image)
        {
            return new HtmlString(UrlResolver.Value.GetUrl(image.ContentLink));
        }

        public static HtmlString ImageExternalUrl(this IUrlHelper urlHelper,
            ImageData image,
            string variant) => urlHelper.ImageExternalUrl(image.ContentLink, variant);

        public static HtmlString ImageExternalUrl(this IUrlHelper urlHelper,
            Uri imageUri,
            string variant)
        {
            return new HtmlString(
                string.IsNullOrWhiteSpace(variant) ? imageUri.ToString() : imageUri + "/" + variant);
        }

        public static HtmlString ImageExternalUrl(this IUrlHelper urlHelper,
            ContentReference imageref,
            string variant)
        {
            if (ContentReference.IsNullOrEmpty(imageref))
                return HtmlString.Empty;

            var url = UrlResolver.Value.GetUrl(imageref);
            //Inject variant
            if (!string.IsNullOrEmpty(variant))
                if (url.Contains("?"))
                    url = url.Insert(url.IndexOf('?'), "/" + variant);
                else
                    url = url + "/" + variant;
            return new HtmlString(url);
        }

        public static HtmlString GetFriendlyUrl(this IUrlHelper urlHelper, string url)
        {
            return new HtmlString(UrlResolver.Value.GetUrl(url) ?? url);

        }

        private static HtmlString WriteShortenedUrl(string root, string segment)
        {
            var fullUrlPath = string.Format("{0}{1}/", root, segment.ToLower().Replace(" ", "-"));

            return new HtmlString(fullUrlPath);
        }

        private static RouteValueDictionary Union(this RouteValueDictionary first,
           RouteValueDictionary second)
        {
            var dictionary = new RouteValueDictionary(second);
            foreach (var pair in first)
                if (pair.Value != null)
                    dictionary[pair.Key] = pair.Value;

            return dictionary;
        }
    }
}