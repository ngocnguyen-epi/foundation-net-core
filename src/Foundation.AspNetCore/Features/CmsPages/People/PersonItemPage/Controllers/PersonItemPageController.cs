using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.CmsPages.People.PersonItemPage.Models;
using Foundation.AspNetCore.Features.CmsPages.People.PersonItemPage.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace Foundation.AspNetCore.Features.CmsPages.People.PersonItemPage.Controllers
{
    public class PersonItemPageController : PageController<PersonPage>
    {
        public ActionResult Index(PersonPage currentPage)
        {
            var model = new PersonItemViewModel(currentPage);
            return View(model);
        }
    }
}
