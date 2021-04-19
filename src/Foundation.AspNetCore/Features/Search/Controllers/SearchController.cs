using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using Foundation.AspNetCore.Cms.Extensions;
using Foundation.AspNetCore.Features.Search.Models;
using Foundation.AspNetCore.Features.Search.Services;
using Foundation.AspNetCore.Features.Search.ViewModelFactories;
using Foundation.AspNetCore.Features.Search.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.Search.Controllers
{
    public class SearchController : PageController<SearchResultPage>
    {
        private readonly ISearchViewModelFactory _viewModelFactory;
        private readonly ISearchService _searchService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContentLoader _contentLoader;

        public SearchController(
            ISearchViewModelFactory viewModelFactory,
            IHttpContextAccessor httpContextAccessor,
            IContentLoader contentLoader,
            ISearchService searchService)
        {
            _viewModelFactory = viewModelFactory;
            _httpContextAccessor = httpContextAccessor;
            _contentLoader = contentLoader;
            _searchService = searchService;
        }

        [AcceptVerbs("GET", "POST")]
        public ActionResult Index(SearchResultPage currentPage, FilterOptionViewModel filterOptions)
        {
            if (filterOptions == null || filterOptions.Q.IsNullOrEmpty())
            {
                return Redirect(Url.ContentUrl(ContentReference.StartPage));
            }

            var selectedFacets = _httpContextAccessor.HttpContext.Request.Query["facets"];
            var viewModel = _viewModelFactory.Create(currentPage, selectedFacets, 0, filterOptions);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult QuickSearch(string search = "")
        {
            var model = new SearchViewModel<SearchResultPage>();
            var contentResult = _searchService.SearchContent(new FilterOptionViewModel()
            {
                Q = search,
                PageSize = 5,
                Page = 1,
                IncludeImagesContent = true
            });
            model.ContentSearchResult = contentResult;

            return View("_QuickSearchContent", model);
        }
    }
}
