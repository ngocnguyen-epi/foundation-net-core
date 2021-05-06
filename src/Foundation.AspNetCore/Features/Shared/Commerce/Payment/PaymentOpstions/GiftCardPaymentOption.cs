﻿using EPiServer.Commerce.Order;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using Foundation.AspNetCore.Features.MyAccount.GiftCard.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Market.Services;
using Foundation.AspNetCore.Features.Shared.Commerce.Payment.Interfaces;
using Mediachase.Commerce;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Orders;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Payment.PaymentOpstions
{
    public class GiftCardPaymentOption : PaymentOptionBase
    {
        private readonly IOrderGroupFactory _orderGroupFactory;
        private Injected<IGiftCardService> _giftCardService;

        public List<SelectListItem> AvailableGiftCards { get; set; }
        public string SelectedGiftCardId { get; set; }

        public GiftCardPaymentOption()
            : this(LocalizationService.Current, ServiceLocator.Current.GetInstance<IOrderGroupFactory>(), ServiceLocator.Current.GetInstance<ICurrentMarket>(), ServiceLocator.Current.GetInstance<LanguageService>(), ServiceLocator.Current.GetInstance<IPaymentService>())
        {
        }

        public GiftCardPaymentOption(LocalizationService localizationService,
            IOrderGroupFactory orderGroupFactory,
            ICurrentMarket currentMarket,
            LanguageService languageService,
            IPaymentService paymentService)
            : base(localizationService, orderGroupFactory, currentMarket, languageService, paymentService)
        {
            _orderGroupFactory = orderGroupFactory;

            AvailableGiftCards = new List<SelectListItem>();
            var isActiveGiftCards = _giftCardService.Service.GetCustomerGiftCards(CustomerContext.Current.CurrentContactId.ToString()).Where(g => g.IsActive == true);

            foreach (var giftCard in isActiveGiftCards)
            {
                AvailableGiftCards.Add(new SelectListItem()
                {
                    Text = giftCard.GiftCardName + " - " + decimal.Round(giftCard.RemainBalance) + " USD",
                    Value = giftCard.GiftCardId
                });
            }
        }

        public override string SystemKeyword => "GiftCardPayment";

        public override IPayment CreatePayment(decimal amount, IOrderGroup orderGroup)
        {
            var payment = _orderGroupFactory.CreatePayment(orderGroup);
            payment.Properties.Add("GiftCardId", SelectedGiftCardId);
            payment.PaymentMethodId = PaymentMethodId;
            payment.PaymentMethodName = "GiftCardPayment";
            payment.Amount = amount;
            payment.Status = PaymentStatus.Pending.ToString();
            payment.TransactionType = TransactionType.Authorization.ToString();
            return payment;
        }

        public override bool ValidateData() => true;
    }
}