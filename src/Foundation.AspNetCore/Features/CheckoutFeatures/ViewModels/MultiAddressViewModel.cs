using Foundation.AspNetCore.Features.CheckoutFeatures.Models;
using Foundation.AspNetCore.Features.MyAccount.AddressBook.Models;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CheckoutFeatures.ViewModels
{
    public class MultiAddressViewModel : ContentViewModel<CheckoutPage>
    {
        public MultiAddressViewModel()
        {
        }

        public MultiAddressViewModel(CheckoutPage multiShipmentPage) : base(multiShipmentPage)
        {
        }

        public IList<AddressModel> AvailableAddresses { get; set; }

        public CartItemViewModel[] CartItems { get; set; }

        public string ReferrerUrl { get; set; }
    }
}