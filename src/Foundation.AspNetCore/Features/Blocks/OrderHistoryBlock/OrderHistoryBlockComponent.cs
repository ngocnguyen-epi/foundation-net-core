using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Logging;
using EPiServer.Security;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.MyAccount.AddressBook.Models;
using Foundation.AspNetCore.Features.MyAccount.AddressBook.Services;
using Foundation.AspNetCore.Features.MyAccount.OrderHistory.ViewModels;
using Foundation.AspNetCore.Features.Settings;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Infrastructure;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Features.Blocks.OrderHistoryBlock
{
    [Authorize]
    [TemplateDescriptor(Inherited = true)]
    public class OrderHistoryBlockComponent : BlockComponent<OrderHistoryBlock>
    {
        private readonly IAddressBookService _addressBookService;
        private readonly IOrderRepository _orderRepository;
        private readonly ISettingsService _settingsService;
        private readonly ICustomerService _customerService;

        public OrderHistoryBlockComponent(IAddressBookService addressBookService, IOrderRepository orderRepository, ISettingsService settingsService, ICustomerService customerService)
        {
            _addressBookService = addressBookService;
            _orderRepository = orderRepository;
            _settingsService = settingsService;
            _customerService = customerService;
        }

        public override IViewComponentResult Invoke(OrderHistoryBlock currentBlock)
        {
            var purchaseOrders = OrderContext.Current.LoadByCustomerId<PurchaseOrder>(PrincipalInfo.CurrentPrincipal.GetContactId())
                                             .OrderByDescending(x => x.Created)
                                             .ToList();

            var viewModel = new OrderHistoryViewModel
            {
                CurrentBlock = currentBlock,
                Orders = new List<OrderViewModel>(),
                CurrentCustomer = _customerService.GetCurrentContact()
            };

            foreach (var purchaseOrder in purchaseOrders)
            {
                //Assume there is only one form per purchase.
                var form = purchaseOrder.GetFirstForm();

                var billingAddress = form.Payments.FirstOrDefault() != null ? form.Payments.First().BillingAddress : new OrderAddress();
                var orderViewModel = new OrderViewModel
                {
                    PurchaseOrder = purchaseOrder,
                    Items = form.GetAllLineItems().Select(lineItem => new OrderHistoryItemViewModel
                    {
                        LineItem = lineItem,
                    }).GroupBy(x => x.LineItem.Code).Select(group => group.First()),
                    BillingAddress = _addressBookService.ConvertToModel(billingAddress),
                    ShippingAddresses = new List<AddressModel>()
                };

                foreach (var orderAddress in form.Shipments.Select(s => s.ShippingAddress))
                {
                    var shippingAddress = _addressBookService.ConvertToModel(orderAddress);
                    orderViewModel.ShippingAddresses.Add(shippingAddress);
                    orderViewModel.OrderGroupId = purchaseOrder.OrderGroupId;
                }

                if (!string.IsNullOrEmpty(purchaseOrder[Constant.Quote.QuoteStatus]?.ToString()) &&
                    (purchaseOrder.Status == OrderStatus.InProgress.ToString() || purchaseOrder.Status == OrderStatus.OnHold.ToString()))
                {
                    orderViewModel.QuoteStatus = purchaseOrder[Constant.Quote.QuoteStatus].ToString();
                    DateTime.TryParse(purchaseOrder[Constant.Quote.QuoteExpireDate].ToString(), out var quoteExpireDate);
                    if (DateTime.Compare(DateTime.Now, quoteExpireDate) > 0)
                    {
                        orderViewModel.QuoteStatus = Constant.Quote.QuoteExpired;
                        try
                        {
                            // Update order quote status to expired
                            purchaseOrder[Constant.Quote.QuoteStatus] = Constant.Quote.QuoteExpired;
                            _orderRepository.Save(purchaseOrder);
                        }
                        catch (Exception ex)
                        {
                            LogManager.GetLogger(GetType()).Error("Failed to update order status to Quote Expired.", ex.StackTrace);
                        }
                    }
                }

                viewModel.Orders.Add(orderViewModel);
            }

            viewModel.OrderDetailsPageUrl =
                UrlResolver.Current.GetUrl(_settingsService.GetSiteSettings<ReferencePageSettings>()?.OrderDetailsPage ?? ContentReference.StartPage);

            return View(viewModel);
        }
    }
}