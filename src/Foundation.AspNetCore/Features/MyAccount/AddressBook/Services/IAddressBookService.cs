using EPiServer.Commerce.Order;
using Foundation.AspNetCore.Features.MyAccount.AddressBook.Models;
using Foundation.AspNetCore.Features.MyAccount.AddressBook.ViewModels;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.ViewModels;
using Foundation.AspNetCore.Features.Shared.Commerce.Organization.ViewModels;
using Mediachase.Commerce.Customers;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyAccount.AddressBook.Services
{
    public interface IAddressBookService
    {
        AddressCollectionViewModel GetAddressBookViewModel(AddressBookPage addressBookPage);
        IList<AddressModel> List();
        bool CanSave(AddressModel addressModel);
        void Save(AddressModel addressModel, FoundationContact contact = null);
        void Delete(string addressId);
        void SetPreferredBillingAddress(string addressId);
        void SetPreferredShippingAddress(string addressId);
        CustomerAddress GetPreferredBillingAddress();
        void LoadAddress(AddressModel addressModel);
        void LoadCountriesAndRegionsForAddress(AddressModel addressModel);
        IEnumerable<string> GetRegionsByCountryCode(string countryCode);
        void MapToAddress(AddressModel addressModel, IOrderAddress orderAddress);
        void MapToAddress(AddressModel addressModel, CustomerAddress customerAddress);
        void MapToModel(CustomerAddress customerAddress, AddressModel addressModel);
        void MapToModel(IOrderAddress orderAddress, AddressModel addressModel);
        IOrderAddress ConvertToAddress(AddressModel addressModel, IOrderGroup orderGroup);
        AddressModel ConvertToModel(IOrderAddress orderAddress);

        IList<AddressModel> MergeAnonymousShippingAddresses(IList<AddressModel> addresses,
            IEnumerable<CartItemViewModel> cartItems);

        bool UseBillingAddressForShipment();
        void UpdateOrganizationAddress(FoundationOrganization organization, B2BAddressViewModel addressModel);
        IEnumerable<CountryViewModel> GetAllCountries();
        string GetCountryNameByCode(string code);
        void DeleteAddress(string organizationId, string addressId);
        AddressModel GetAddress(string addressId);
        AddressModel ConvertAddress(CustomerAddress customerAddress);
    }
}