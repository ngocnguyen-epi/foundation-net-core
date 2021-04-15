using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.CmsPages.LandingPages.TwoColumnLandingPage.Controllers
{
    public class TwoColumnLandingPageController : PageController<Models.TwoColumnLandingPage>
    {
        public ActionResult Index(Models.TwoColumnLandingPage currentPage)
        {
            var model = ContentViewModel.Create(currentPage);
            return View(model);
        }
    }
}