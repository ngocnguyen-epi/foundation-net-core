using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.CmsPages.LandingPages.ThreeColumnLandingPage.Controllers
{
    public class ThreeColumnLandingPageController : PageController<Models.ThreeColumnLandingPage>
    {
        public ActionResult Index(Models.ThreeColumnLandingPage currentPage)
        {
            var model = ContentViewModel.Create(currentPage);
            return View(model);
        }
    }
}