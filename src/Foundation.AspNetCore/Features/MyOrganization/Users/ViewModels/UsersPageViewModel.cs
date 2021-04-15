using Foundation.AspNetCore.Features.MyOrganization.Users.Models;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Organization.Models;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyOrganization.Users.ViewModels
{
    public class UsersPageViewModel : ContentViewModel<UsersPage>
    {
        public List<FoundationContact> Users { get; set; }
        public FoundationContact Contact { get; set; }
        public List<FoundationOrganization> Organizations { get; set; }
        public SubFoundationOrganizationModel SubOrganization { get; set; }
    }
}
