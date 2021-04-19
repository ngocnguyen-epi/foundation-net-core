using EPiServer.Core;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Pricing;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Market.Interfaces
{
    public interface IPromotionService
    {
        IList<IPriceValue> GetDiscountPriceList(IEnumerable<CatalogKey> catalogKeys, MarketId marketId, Currency currency);
        IPriceValue GetDiscountPrice(CatalogKey catalogKey, MarketId marketId, Currency currency);
        IPriceValue GetDiscountPrice(IPriceValue price, ContentReference contentLink, Currency currency, IMarket market);
    }
}