using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Features.MyAccount.AddressBook.Services;
using Foundation.AspNetCore.Features.MyAccount.OrderConfirmation.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.MyAccount.OrderConfirmation.Controllers
{
    public class OrderConfirmationController : OrderConfirmationControllerBase<OrderConfirmationPage>
    {
        //private readonly ICampaignService _campaignService;
        private readonly IContextModeResolver _contextModeResolver;
        public OrderConfirmationController(
            //ICampaignService campaignService,
            IConfirmationService confirmationService,
            IAddressBookService addressBookService,
            IOrderGroupCalculator orderGroupCalculator,
            UrlResolver urlResolver, ICustomerService customerService) :
            base(confirmationService, addressBookService, orderGroupCalculator, urlResolver, customerService)
        {
            //_campaignService = campaignService;
        }
        public IActionResult Index(OrderConfirmationPage currentPage, string notificationMessage, int? orderNumber)
        {
            IPurchaseOrder order = null;
            if (_contextModeResolver.CurrentMode.EditOrPreview())
            {
                order = _confirmationService.CreateFakePurchaseOrder();
            }
            else if (orderNumber.HasValue)
            {
                order = _confirmationService.GetOrder(orderNumber.Value);
            }

            if (order != null && order.CustomerId == _customerService.CurrentContactId)
            {
                var viewModel = CreateViewModel(currentPage, order);
                viewModel.NotificationMessage = notificationMessage;

                //_campaignService.UpdateLastOrderDate();
                //_campaignService.UpdatePoint(decimal.ToInt16(viewModel.SubTotal.Amount));

                return View(viewModel);
            }

            return Redirect(Url.ContentUrl(ContentReference.StartPage));
        }
    }
}