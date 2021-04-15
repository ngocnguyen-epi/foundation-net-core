using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.MyOrganization.QuickOrderPage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.MyOrganization.QuickOrderPage.Controllers
{
    [Authorize]
    public class QuickOrderPageController : PageController<Models.QuickOrderPage>
    {
        public IActionResult Index(Models.QuickOrderPage currentPage)
        {
            return View(new QuickOrderPageViewModel
            {
                CurrentContent = currentPage
            });
        }
    }
}