using EPiServer.Commerce.Order;

namespace Foundation.AspNetCore.Features.MyAccount.OrderHistory.ViewModels
{
    public class OrderHistoryItemViewModel
    {
        public ILineItem LineItem { get; set; }
    }
}