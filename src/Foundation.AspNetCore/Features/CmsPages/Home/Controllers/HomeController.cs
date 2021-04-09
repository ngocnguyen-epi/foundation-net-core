using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.CmsPages.Home
{
    public class HomeController : PageController<HomePage>
    {
        public ActionResult Index(HomePage currentContent)
        {
            return View(ContentViewModel.Create<HomePage>(currentContent));
        }
    }
}
