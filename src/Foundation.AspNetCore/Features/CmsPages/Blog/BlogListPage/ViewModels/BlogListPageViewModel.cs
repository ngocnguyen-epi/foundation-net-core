using Foundation.AspNetCore.Features.CmsPages.Blog.BlogItemPage.ViewModels;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.Models;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CmsPages.Blog.BlogListPage.ViewModels
{
    public class BlogListPageViewModel : ContentViewModel<Models.BlogListPage>
    {
        public BlogListPageViewModel(Models.BlogListPage currentPage) : base(currentPage)
        {
            Heading = currentPage.Heading;
            ShowIntroduction = currentPage.IncludeTeaserText;
            ShowPublishDate = currentPage.IncludePublishDate;
        }

        public List<KeyValuePair<string, string>> SubNavigation { get; set; }
        public string Heading { get; set; }
        public IEnumerable<BlogItemPageViewModel> Blogs { get; set; }
        public bool ShowIntroduction { get; set; }
        public bool ShowPublishDate { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
