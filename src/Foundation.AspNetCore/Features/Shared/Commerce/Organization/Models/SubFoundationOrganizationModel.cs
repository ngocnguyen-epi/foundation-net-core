using Foundation.AspNetCore.Features.MyAccount.AddressBook.ViewModels;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Organization.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Organization.Models
{
    public class SubFoundationOrganizationModel : FoundationOrganization
    {
        public SubFoundationOrganizationModel(FoundationOrganization organization) : base(organization.OrganizationEntity)
        {
            Name = organization.Name;
            Locations = organization.Addresses != null && organization.Addresses.Any()
                ? organization.Addresses.Select(address => new B2BAddressViewModel(address)).ToList()
                : new List<B2BAddressViewModel>();
        }

        //[LocalizedDisplay("/B2B/Organization/SubOrganizationName")]
        //[LocalizedRequired("/B2B/Organization/SubOrganizationNameRequired")]
        public new string Name { get; set; }

        public List<B2BAddressViewModel> Locations { get; set; }
        public IEnumerable<CountryViewModel> CountryOptions { get; set; }
    }
}