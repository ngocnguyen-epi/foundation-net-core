using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Extensions;
using Foundation.AspNetCore.Features.CatalogContents.Package.Models;
using Foundation.AspNetCore.Features.CatalogContents.Package.ViewModels;
using Foundation.AspNetCore.Features.CatalogContents.Shared.Controllers;
using Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels;
using Foundation.AspNetCore.Features.CatalogContents.Variation.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Features.Shared.Personalization;
using Mediachase.Commerce.Catalog;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.CatalogContents.Package.Controllers
{
    public class PackageController : CatalogContentControllerBase<GenericPackage>
    {
        private readonly CatalogEntryViewModelFactory _viewModelFactory;

        public PackageController(CatalogEntryViewModelFactory viewModelFactory,
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
        public async Task<IActionResult> Index(GenericPackage currentContent, bool skipTracking = false)
        {
            var viewModel = _viewModelFactory.CreatePackage<GenericPackage, GenericVariant, GenericPackageViewModel>(currentContent);
            viewModel.BreadCrumb = GetBreadCrumb(currentContent.Code);
            if (_isInEditMode && !viewModel.Entries.Any())
            {
                return View(viewModel);
            }

            if (viewModel.Entries == null || !viewModel.Entries.Any())
            {
                return NotFound();
            }

            await AddInfomationViewModel(viewModel, currentContent.Code, skipTracking);
            currentContent.AddBrowseHistory();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult QuickView(string productCode)
        {
            var currentContentRef = _referenceConverter.GetContentLink(productCode);
            var currentContent = _contentLoader.Get<EntryContentBase>(currentContentRef) as GenericPackage;
            if (currentContent != null)
            {
                var viewModel = _viewModelFactory.CreatePackage<GenericPackage, GenericVariant, GenericPackageViewModel>(currentContent);
                return PartialView("_QuickView", viewModel);
            }

            return StatusCode(404, "Product not found.");
        }
    }
}