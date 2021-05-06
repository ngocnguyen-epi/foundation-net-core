using EPiServer.Commerce.Order;
using Foundation.AspNetCore.Features.Shared.Commerce.Shipping.Models;
using Mediachase.Commerce;
using Mediachase.Commerce.Orders;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Shipping.Interfaces
{
    public interface IShippingService
    {
        IList<ShippingMethodInfoModel> GetShippingMethodsByMarket(string marketid, bool returnInactive);
        ShippingMethodInfoModel GetInstorePickupModel();
        ShippingRate GetRate(IShipment shipment, ShippingMethodInfoModel shippingMethodInfoModel, IMarket currentMarket);
    }
}
