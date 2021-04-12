using EPiServer.DataAbstraction;
using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.CmsPages.StandardPage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.CmsPages.StandardPage.Controllers
{
    public class StandardPageController : PageController<Models.StandardPage>
    {
        private readonly CategoryRepository _categoryRepository;

        public StandardPageController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //[PageViewTracking]
        public IActionResult Index(Models.StandardPage currentPage)
        {
            var model = StandardPageViewModel.Create(currentPage, _categoryRepository);
            return View(model);
        }
    }
}
