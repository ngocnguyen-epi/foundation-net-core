using Foundation.AspNetCore.Features.MyAccount.AddressBook.Models;
using Foundation.AspNetCore.Features.Shared;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyAccount.AddressBook.ViewModels
{
    public class AddressCollectionViewModel : ContentViewModel<AddressBookPage>
    {
        public AddressCollectionViewModel()
        {
        }

        public AddressCollectionViewModel(AddressBookPage currentPage) : base(currentPage) { }

        public IEnumerable<AddressModel> Addresses { get; set; }
    }
}