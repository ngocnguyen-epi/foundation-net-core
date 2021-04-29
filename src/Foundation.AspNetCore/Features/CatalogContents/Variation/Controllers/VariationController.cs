using EPiServer;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Features.CatalogContents.Shared.Controllers;
using Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels;
using Foundation.AspNetCore.Features.CatalogContents.Variation.Models;
using Foundation.AspNetCore.Features.CatalogContents.Variation.ViewModels;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Features.Shared.Personalization;
using Mediachase.Commerce.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.CatalogContents.Variation.Controllers
{
    [TemplateDescriptor(Inherited = true)]
    public class VariationController : CatalogContentControllerBase<GenericVariant>
    {
        private readonly CatalogEntryViewModelFactory _viewModelFactory;

        public VariationController(CatalogEntryViewModelFactory viewModelFactory,
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
        public IActionResult Index(GenericVariant currentContent)
        {
            var viewModel = _viewModelFactory.CreateVariant<GenericVariant, GenericVariantViewModel>(currentContent);
            viewModel.BreadCrumb = GetBreadCrumb(currentContent.Code);
            return View(viewModel);
        }
    }
}