using EPiServer.Core;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CmsPages.Location.TagPage.ViewModels
{
    public class TagsCarouselViewModel
    {
        public List<TagsCarouselItem> Items { get; set; }
    }

    public class TagsCarouselItem
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public ContentReference Image { get; set; }
        public ContentReference ItemURL { get; set; }
    }
}