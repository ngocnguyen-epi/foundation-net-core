using Mediachase.Commerce;
using System;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Shipping.ViewModels
{
    public class ShippingMethodViewModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public Money Price { get; set; }
        public bool IsInstorePickup { get; set; }
    }
}