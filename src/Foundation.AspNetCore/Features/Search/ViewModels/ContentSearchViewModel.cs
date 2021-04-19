using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Search.ViewModels
{
    public class ContentSearchViewModel
    {
        public IEnumerable<SearchHit> Hits { get; set; }
        public FilterOptionViewModel FilterOption { get; set; }

        //public string SectionFilter => HttpContext.Current.Request.QueryString["t"] ?? string.Empty;

        //public string GetSectionGroupUrl(string groupName)
        //{
        //    var url = UriUtil.AddQueryString(HttpContext.Current.Request.RawUrl, "t", HttpContext.Current.Server.UrlEncode(groupName));
        //    url = UriUtil.AddQueryString(url, "p", "1");
        //    return url;
        //}
    }

    public class SearchHit
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Excerpt { get; set; }
        public string ImageUri { get; set; }      
    }
}
