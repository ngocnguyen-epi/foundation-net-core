using Foundation.AspNetCore.Infrastructure.Attributes;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyAccount.AddressBook.ViewModels
{
    public class CountryRegionViewModel
    {
        public IEnumerable<string> RegionOptions { get; set; }

        [LocalizedDisplay("/Shared/Address/Form/Label/CountryRegion")]
        public string Region { get; set; }

        public string SelectClass { get; set; } = "select";
        public string TextboxClass { get; set; } = "textbox";
    }
}