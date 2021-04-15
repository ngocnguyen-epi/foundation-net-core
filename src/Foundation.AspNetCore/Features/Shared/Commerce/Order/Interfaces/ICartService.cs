using EPiServer.Commerce.Order;
using Foundation.AspNetCore.Features.MyAccount.AddressBook.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.ViewModels;
using Foundation.AspNetCore.Features.Shared.Commerce.Shipping.Models;
using Mediachase.Commerce;
using System;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Order.Interfaces
{
    public interface ICartService
    {
        ShippingMethodInfoModel InStorePickupInfoModel { get; }
        AddToCartResult AddToCart(ICart cart, RequestParamsToCart requestParams);
        Dictionary<ILineItem, List<ValidationIssue>> ChangeCartItem(ICart cart, int shipmentId, string code, decimal quantity, string size, string newSize);
        void SetCartCurrency(ICart cart, Currency currency);
        Dictionary<ILineItem, List<ValidationIssue>> ValidateCart(ICart cart);
        Dictionary<ILineItem, List<ValidationIssue>> RequestInventory(ICart cart);
        string DefaultCartName { get; }
        string DefaultWishListName { get; }
        string DefaultSharedCartName { get; }
        string DefaultOrderPadName { get; }
        CartWithValidationIssues LoadCart(string name, bool validate);
        CartWithValidationIssues LoadCart(string name, string contactId, bool validate);
        ICart LoadOrCreateCart(string name);
        ICart LoadOrCreateCart(string name, string contactId);
        bool AddCouponCode(ICart cart, string couponCode);
        void RemoveCouponCode(ICart cart, string couponCode);
        void RecreateLineItemsBasedOnShipments(ICart cart, IEnumerable<CartItemViewModel> cartItems, IEnumerable<AddressModel> addresses);
        void MergeShipments(ICart cart);
        ICart LoadWishListCardByCustomerId(Guid currentContactId);
        ICart LoadSharedCardByCustomerId(Guid currentContactId);
        Dictionary<ILineItem, List<ValidationIssue>> ChangeQuantity(ICart cart, int shipmentId, string code, decimal quantity);
        Money? GetDiscountedPrice(ICart cart, ILineItem lineItem);
        ICart CreateNewCart();
        void DeleteCart(ICart cart);
        bool PlaceCartForQuote(ICart cart);
        ICart PlaceOrderToCart(IPurchaseOrder purchaseOrder, ICart cart);
        void RemoveQuoteNumber(ICart cart);
        int PlaceCartForQuoteById(int orderId, Guid userId);
        AddToCartResult SeparateShipment(ICart cart, string code, int quantity, int fromShipmentId, int toShipmentId, string deliveryMethodId, string warehouseCode);
    }
}