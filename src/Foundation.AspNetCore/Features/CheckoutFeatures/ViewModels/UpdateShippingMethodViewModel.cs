using Foundation.AspNetCore.Features.Shared.Commerce.Shipping.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CheckoutFeatures.ViewModels
{
    public class UpdateShippingMethodViewModel
    {
        public IList<ShipmentViewModel> Shipments { get; set; }
    }
}