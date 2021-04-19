using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.CmsPages.Events.CalendarEventPage.Controllers
{
    public class CalendarEventPageController : PageController<Models.CalendarEventPage>
    {
        public ActionResult Index(Models.CalendarEventPage currentPage) => View(ContentViewModel.Create(currentPage));
    }
}