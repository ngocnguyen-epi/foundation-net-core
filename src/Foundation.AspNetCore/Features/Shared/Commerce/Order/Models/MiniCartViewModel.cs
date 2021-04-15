using EPiServer.Core;
using Foundation.AspNetCore.Features.Shared.Commerce.Shipping.Models;
using Mediachase.Commerce;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Order.Models
{
    public class MiniCartViewModel
    {
        public MiniCartViewModel()
        {
            Shipments = new List<ShipmentViewModel>();
        }

        public ContentReference CheckoutPage { get; set; }

        public ContentReference CartPage { get; set; }

        public decimal ItemCount { get; set; }

        public IEnumerable<ShipmentViewModel> Shipments { get; set; }

        public Money Total { get; set; }

        public string Label { get; set; }

        public bool IsSharedCart { get; set; }
    }
}