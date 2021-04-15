using Foundation.AspNetCore.Features.MyOrganization.SubOrganization.Models;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.Commerce.Organization.Models;

namespace Foundation.AspNetCore.Features.MyOrganization.SubOrganization.ViewModels
{
    public class SubOrganizationPageViewModel : ContentViewModel<SubOrganizationPage>
    {
        public SubOrganizationModel SubOrganizationModel { get; set; }
        public bool IsAdmin { get; set; }
    }
}
