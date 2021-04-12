using Foundation.AspNetCore.Features.MyAccount.AddressBook.Models;
using Foundation.AspNetCore.Features.Shared;

namespace Foundation.AspNetCore.Features.MyAccount.AddressBook.ViewModels
{
    public class AddressViewModel : ContentViewModel<AddressBookPage>
    {
        public AddressModel Address { get; set; }

        public AddressViewModel(AddressBookPage currentPage) : base(currentPage) { }
        public AddressViewModel() : base() { }
    }
}