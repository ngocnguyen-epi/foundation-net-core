using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Cms.Extensions;
using Foundation.AspNetCore.Features.Search.ViewModels;
using Foundation.AspNetCore.Features.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Features.Search.Services
{
    [ServiceConfiguration(typeof(ISearchService), Lifecycle = ServiceInstanceScope.Transient)]
    public class SearchService : ISearchService
    {
        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;

        public SearchService(IContentLoader contentLoader,
            UrlResolver urlResolver)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
        }

        public ContentSearchViewModel SearchContent(FilterOptionViewModel filterOptions)
        {
            var model = new ContentSearchViewModel
            {
                FilterOption = filterOptions,
                Hits =  new List<SearchHit>()
            };
            if (!filterOptions.Q.IsNullOrEmpty())
            {
                var siteId = SiteDefinition.Current.Id;
                var contentReferences = _contentLoader.GetDescendents(ContentReference.StartPage);
                var result = contentReferences.Select(x => _contentLoader.Get<IContent>(x))
                    .OfType<FoundationPageData>()
                    .Where(x => x.Name.ToLowerInvariant().StartsWith(filterOptions.Q))
                    .Select(x => new SearchHit { 
                        Title = x.Name,
                        Url = _urlResolver.GetUrl(x.ContentLink),
                        Excerpt = x.PageDescription,
                        ImageUri = _urlResolver.GetUrl(x.PageImage)
                    })
                    .Skip((filterOptions.Page - 1) * filterOptions.PageSize)
                    .Take(filterOptions.PageSize);
                model.Hits = result;
            }

            return model;
        }
    }
}
