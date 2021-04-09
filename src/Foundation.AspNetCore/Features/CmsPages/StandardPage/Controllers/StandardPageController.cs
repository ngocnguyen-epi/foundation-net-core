using EPiServer.DataAbstraction;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.StandardPage
{
    public class StandardPageController : PageController<StandardPage>
    {
        private readonly CategoryRepository _categoryRepository;

        public StandardPageController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //[PageViewTracking]
        public IActionResult Index(StandardPage currentPage)
        {
            var model = StandardPageViewModel.Create(currentPage, _categoryRepository);
            return View(model);
        }
    }
}
