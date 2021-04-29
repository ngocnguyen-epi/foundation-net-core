using EPiServer;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Extensions;
using Foundation.AspNetCore.Features.CatalogContents.DynamicCatalogContent.Models;
using Foundation.AspNetCore.Features.CatalogContents.DynamicCatalogContent.ViewModels;
using Foundation.AspNetCore.Features.CatalogContents.Shared.Controllers;
using Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Features.Shared.Personalization;
using Mediachase.Commerce.Catalog;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.CatalogContents.DynamicCatalogContent.Controllers
{
    public class DynamicProductController : CatalogContentControllerBase<DynamicProduct>
    {
        private readonly CatalogEntryViewModelFactory _viewModelFactory;

        public DynamicProductController(CatalogEntryViewModelFactory viewModelFactory,
            IReviewService reviewService,
            IReviewActivityService reviewActivityService,
            ICommerceTrackingService recommendationService,
            ReferenceConverter referenceConverter,
            IContentLoader contentLoader,
            UrlResolver urlResolver,
            ILoyaltyService loyaltyService) : base(referenceConverter, contentLoader, urlResolver, reviewService, reviewActivityService, recommendationService, loyaltyService)
        {
            _viewModelFactory = viewModelFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DynamicProduct currentContent, string variationCode = "", bool skipTracking = false)
        {
            var viewModel = _viewModelFactory.Create<DynamicProduct, DynamicVariant, DynamicProductViewModel>(currentContent, variationCode);

            if (_isInEditMode && viewModel.Variant == null)
            {
                return View(viewModel);
            }

            if (viewModel.Variant == null)
            {
                return NotFound();
            }

            viewModel.GenerateVariantGroup();
            await AddInfomationViewModel(viewModel, currentContent.Code, skipTracking);
            currentContent.AddBrowseHistory();
            viewModel.BreadCrumb = GetBreadCrumb(currentContent.Code);
            return View("", viewModel);
        }
    }
}