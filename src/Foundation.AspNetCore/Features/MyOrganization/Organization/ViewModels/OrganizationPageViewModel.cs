using EPiServer.Core;
using Foundation.AspNetCore.Features.MyOrganization.Organization.Models;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.Commerce.Organization.Models;

namespace Foundation.AspNetCore.Features.MyOrganization.Organization.ViewModels
{
    public class OrganizationPageViewModel : ContentViewModel<OrganizationPage>
    {
        public OrganizationModel Organization { get; set; }
        public SubOrganizationModel NewSubOrganization { get; set; }
        public ContentReference SubOrganizationPage { get; set; }
        public bool IsAdmin { get; set; }
    }
}
