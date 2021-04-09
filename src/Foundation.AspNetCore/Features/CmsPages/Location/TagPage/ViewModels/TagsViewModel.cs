using Foundation.AspNetCore.Features.Shared;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CmsPages.Location.TagPage.ViewModels
{
    public class TagsViewModel : ContentViewModel<Models.TagPage>
    {
        public TagsViewModel(Models.TagPage currentPage) : base(currentPage)
        {
        }

        public string Continent { get; set; }

        public string[] AdditionalCategories { get; set; }

        public TagsCarouselViewModel Carousel { get; set; }

        public List<LocationItemPage.Models.LocationItemPage> Locations { get; set; }
    }
}