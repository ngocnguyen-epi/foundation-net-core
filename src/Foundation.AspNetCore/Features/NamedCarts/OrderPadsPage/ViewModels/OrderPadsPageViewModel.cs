using EPiServer.Commerce.Order;
using Foundation.AspNetCore.Features.CheckoutFeatures.ViewModels;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.NamedCarts.OrderPadsPage.ViewModels
{
    public class OrderPadsPageViewModel : ContentViewModel<Models.OrderPadsPage>
    {
        public string QuoteStatus { get; set; }
        public FoundationContact CurrentCustomer { get; set; }
        public List<ICart> OrderPardCartsList { get; set; }
        public List<OrganizationOrderPadViewModel> OrganizationOrderPadList { get; set; }
    }
}