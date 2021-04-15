using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.CmsPages.LandingPages.LandingPage.Controllers
{
    public class LandingPageController : PageController<Models.LandingPage>
    {
        public ActionResult Index(Models.LandingPage currentPage)
        {
            var model = ContentViewModel.Create(currentPage);
            return View(model);
        }
    }
}