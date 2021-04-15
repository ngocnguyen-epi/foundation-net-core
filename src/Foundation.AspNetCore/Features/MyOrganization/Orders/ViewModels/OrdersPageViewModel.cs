using Foundation.AspNetCore.Features.MyOrganization.Orders.Models;
using Foundation.AspNetCore.Features.Shared;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyOrganization.Orders.ViewModels
{
    public class OrdersPageViewModel : ContentViewModel<OrdersPage>
    {
        public List<OrderOrganizationViewModel> OrdersOrganization { get; set; }
        public string OrderDetailsPageUrl { get; set; }
    }
}