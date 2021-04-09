using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.CmsPages.Location.TagPage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CmsPages.Location.TagPage.Controllers
{
    public class TagPageController : PageController<Models.TagPage>
    {
        public ActionResult Index(Models.TagPage currentPage)
        {
            var model = new TagsViewModel(currentPage) {
                Locations = new List<LocationItemPage.Models.LocationItemPage>()
            };

            //Add theme images from results
            var carousel = new TagsCarouselViewModel
            {
                Items = new List<TagsCarouselItem>()
            };
            model.Carousel = carousel;

            return View(model);
        }
    }
}