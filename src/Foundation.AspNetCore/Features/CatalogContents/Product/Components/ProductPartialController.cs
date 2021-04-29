using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Extensions;
using Foundation.AspNetCore.Features.Shared.Commerce.Market.Interfaces;
using Mediachase.Commerce;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.CatalogContents.Product.Controllers
{
    [TemplateDescriptor(Inherited = true)]
    public class ProductPartialController : PartialContentComponent<EntryContentBase>
    {
        private readonly ICurrentMarket _currentMarket;
        private readonly ICurrencyService _currencyService;

        public ProductPartialController(ICurrentMarket currentMarket,
            ICurrencyService currencyService)
        {
            _currentMarket = currentMarket;
            _currencyService = currencyService;
        }

        [AcceptVerbs(new string[] { "GET", "POST" })]
        public override IViewComponentResult Invoke(EntryContentBase currentContent)
        {
            var productTileViewModel = currentContent.GetProductTileViewModel(_currentMarket.GetCurrentMarket(), _currencyService.GetCurrentCurrency());
            return View("_Product", productTileViewModel);
        }
    }
}