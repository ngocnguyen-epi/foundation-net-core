using Foundation.AspNetCore.Features.Shared;
using System.Collections.Generic;
using Foundation.AspNetCore.Infrastructure.Identity;
using Foundation.AspNetCore.Features.MyAccount.OrderHistory.ViewModels;
using Foundation.AspNetCore.Features.MyAccount.AddressBook.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;

namespace Foundation.AspNetCore.Features.MyAccount.ProfilePage.ViewModels
{
    public class ProfilePageViewModel : ContentViewModel<Models.ProfilePage>
    {
        public ProfilePageViewModel()
        {
        }

        public ProfilePageViewModel(Models.ProfilePage profilePage) : base(profilePage)
        {
        }

        public List<OrderViewModel> Orders { get; set; }
        public IEnumerable<AddressModel> Addresses { get; set; }
        public SiteUser SiteUser { get; set; }
        public FoundationContact CustomerContact { get; set; }
        public string OrderDetailsPageUrl { get; set; }
        public string ResetPasswordPage { get; set; }
        public string AddressBookPage { get; set; }
    }
}