using Mediachase.Commerce;

namespace Foundation.AspNetCore.Features.CheckoutFeatures.ViewModels
{
    public class OrderDiscountViewModel
    {
        public Money Discount { get; set; }
        public string DisplayName { get; set; }
    }
}