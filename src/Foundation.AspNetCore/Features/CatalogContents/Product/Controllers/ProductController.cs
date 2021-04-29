using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Extensions;
using Foundation.AspNetCore.Features.CatalogContents.Product.Models;
using Foundation.AspNetCore.Features.CatalogContents.Product.ViewModels;
using Foundation.AspNetCore.Features.CatalogContents.Shared.Controllers;
using Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels;
using Foundation.AspNetCore.Features.CatalogContents.Variation.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Features.Shared.Personalization;
using Mediachase.Commerce.Catalog;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.CatalogContents.Product.Controllers
{
    public class ProductController : CatalogContentControllerBase<GenericProduct>
    {
        private readonly CatalogEntryViewModelFactory _viewModelFactory;

        public ProductController(CatalogEntryViewModelFactory viewModelFactory,
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
        public async Task<IActionResult> Index(GenericProduct currentContent, string variationCode = "", bool skipTracking = false)
        {
            var viewModel = _viewModelFactory.Create<GenericProduct, GenericVariant, GenericProductViewModel>(currentContent, variationCode);

            if (_isInEditMode && viewModel.Variant == null)
            {
                return View(viewModel);
            }

            if (viewModel.Variant == null)
            {
                return NotFound();
            }

            await AddInfomationViewModel(viewModel, currentContent.Code, skipTracking);
            currentContent.AddBrowseHistory();
            viewModel.BreadCrumb = GetBreadCrumb(currentContent.Code);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult QuickView(string productCode, string variantCode)
        {
            var currentContentRef = _referenceConverter.GetContentLink(productCode);
            var currentContent = _contentLoader.Get<ProductContent>(currentContentRef) as GenericProduct;
            if (currentContent != null)
            {
                var viewModel = _viewModelFactory.Create<GenericProduct, GenericVariant, GenericProductViewModel>(currentContent, variantCode);
                return PartialView("_QuickView", viewModel);
            }

            return StatusCode(404, "Product not found.");
        }

        [HttpGet]
        public IActionResult SelectVariant(string productCode, string color, string size, bool isQuickView = true)
        {
            var currentContentRef = _referenceConverter.GetContentLink(productCode);
            var currentContent = _contentLoader.Get<ProductContent>(currentContentRef) as GenericProduct;
            if (currentContent != null)
            {
                var variant = _viewModelFactory.SelectVariant(currentContent, color, size);
                if (variant != null)
                {
                    var viewModel = _viewModelFactory.Create<GenericProduct, GenericVariant, GenericProductViewModel>(currentContent, variant.Code);

                    if (isQuickView)
                    {
                        return PartialView("_QuickView", viewModel);
                    }
                    else
                    {
                        return PartialView("_ProductDetail", viewModel);
                    }
                }
            }

            return StatusCode(404, "Product not found.");
        }
    }
}