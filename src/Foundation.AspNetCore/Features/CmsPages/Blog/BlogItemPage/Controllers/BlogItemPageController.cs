using EPiServer;
using EPiServer.Cms.Shell;
using EPiServer.Core.Html;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Features.CmsPages.Blog.BlogItemPage.ViewModels;
using Foundation.AspNetCore.Features.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static Foundation.AspNetCore.Features.CmsPages.Blog.BlogItemPage.ViewModels.BlogItemPageViewModel;

namespace Foundation.AspNetCore.Features.CmsPages.Blog.BlogItemPage.Controllers
{
    public class BlogItemPageController : PageController<Models.BlogItemPage>
    {
        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;

        public int PreviewTextLength { get; set; }

        public BlogItemPageController(IContentLoader contentLoader,
            UrlResolver urlResolver)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
        }

        public IActionResult Index(Models.BlogItemPage currentPage)
        {
            PreviewTextLength = 200;

            var model = new BlogItemPageViewModel(currentPage)
            {
                Category = currentPage.Category,
                Tags = /*GetTags(currentPage)*/ new List<TagItem>(),
                PreviewText = GetPreviewText(currentPage),
                MainBody = currentPage.MainBody,
                StartPublish = currentPage.StartPublish ?? DateTime.UtcNow,
                BreadCrumbs = GetBreadCrumb(currentPage)
            };

            var editHints = ViewData.GetEditHints<ContentViewModel<Models.BlogItemPage>, Models.BlogItemPage>();
            editHints.AddConnection(m => m.CurrentContent.Category, p => p.Category);
            editHints.AddConnection(m => m.CurrentContent.StartPublish, p => p.StartPublish);

            return View(model);
        }

        // Geta Categories

        //public IEnumerable<BlogItemPageViewModel.TagItem> GetTags(BlogItemPage currentPage)
        //{
        //    if (currentPage.Categories != null)
        //    {
        //        var allCategories = _contentLoader.GetItems(currentPage.Categories, CultureInfo.CurrentUICulture);
        //        return allCategories.
        //            Select(cat => new BlogItemPageViewModel.TagItem()
        //            {
        //                Title = cat.Name,
        //                Url = _blogTagFactory.GetTagUrl(currentPage, cat.ContentLink),
        //                DisplayName = (cat as StandardCategory)?.Description,
        //            }).ToList();
        //    }
        //    return new List<BlogItemPageViewModel.TagItem>();
        //}

        private string GetPreviewText(Models.BlogItemPage page)
        {
            if (PreviewTextLength <= 0)
            {
                return string.Empty;
            }

            var previewText = string.Empty;

            if (page.MainBody != null)
            {
                previewText = page.MainBody.ToHtmlString();
            }

            if (string.IsNullOrEmpty(previewText))
            {
                return string.Empty;
            }

            var regexPattern = new StringBuilder(@"<span[\s\W\w]*?classid=""");
            //regexPattern.Append(DynamicContentFactory.Instance.DynamicContentId.ToString());
            regexPattern.Append(@"""[\s\W\w]*?</span>");
            previewText = Regex.Replace(previewText, regexPattern.ToString(), string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return TextIndexer.StripHtml(previewText, PreviewTextLength);
        }

        private List<KeyValuePair<string, string>> GetBreadCrumb(Models.BlogItemPage currentPage)
        {
            var breadCrumb = new List<KeyValuePair<string, string>>();
            var ancestors = _contentLoader.GetAncestors(currentPage.ContentLink)
                .Select(x => x as BlogListPage.Models.BlogListPage)
                .Where(x => x != null);
            breadCrumb = ancestors.Reverse().Select(x => new KeyValuePair<string, string>(x.MetaTitle, x.PublicUrl(_urlResolver))).ToList();

            return breadCrumb;
        }
    }
}