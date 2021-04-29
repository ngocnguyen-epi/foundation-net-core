using EPiServer;
using EPiServer.Commerce.Marketing;
using EPiServer.Commerce.Marketing.Internal;
using EPiServer.Commerce.Order;
using EPiServer.Commerce.Order.Internal;
using EPiServer.Core;
using EPiServer.Framework.Cache;
using EPiServer.Security;
using Foundation.AspNetCore.Features.Shared.Commerce.Market.Services;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Marketing;
using Mediachase.Commerce.Pricing;
using Mediachase.Commerce.Security;
using System.Linq;

namespace Foundation.AspNetCore.Features.CatalogContents.Shared.Services
{
    public class FoundationPromotionEngineContentLoader : PromotionEngineContentLoader
    {
        private readonly ReferenceConverter _referenceConverter;

        public FoundationPromotionEngineContentLoader(
          IContentLoader contentLoader,
          CampaignInfoExtractor campaignInfoExtractor,
          IPriceService priceService,
          ReferenceConverter referenceConverter,
          ISynchronizedObjectInstanceCache objectInstanceCache,
          MarketingOptions marketingOptions,
          IContentCacheKeyCreator contentCacheKeyCreator) : base(contentLoader, campaignInfoExtractor, priceService, referenceConverter, objectInstanceCache, marketingOptions, contentCacheKeyCreator)
        {
            _referenceConverter = referenceConverter;
        }

        public override IOrderGroup CreateInMemoryOrderGroup(
          ContentReference entryLink,
          IMarket market,
          Currency marketCurrency)
        {
            InMemoryOrderGroup memoryOrderGroup = new InMemoryOrderGroup(market, marketCurrency);
            memoryOrderGroup.CustomerId = PrincipalInfo.CurrentPrincipal.GetContactId();
            string code = _referenceConverter.GetCode(entryLink);
            IPriceValue price = PriceCalculationService.GetSalePrice(code, market.MarketId, marketCurrency);
            if (price != null && price.UnitPrice != null)
            {
                decimal priceAmount = price.UnitPrice.Amount;
                memoryOrderGroup.Forms.First().Shipments.First().LineItems.Add(new InMemoryLineItem()
                {
                    Quantity = 1M,
                    Code = code,
                    PlacedPrice = priceAmount
                });
            }

            return memoryOrderGroup;
        }
    }
}
