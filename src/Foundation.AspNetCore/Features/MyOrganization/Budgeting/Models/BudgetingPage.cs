using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Business.UIDescriptors;
using Foundation.AspNetCore.Features.Shared;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.MyOrganization.Budgeting.Models
{
    [ContentType(DisplayName = "Budgeting Page",
        GUID = "0ad21ec9-3753-4e2f-9ee8-61e8cba45fe3",
        Description = "Manage budgets for organization.",
        GroupName = GroupNames.Commerce,
        AvailableInEditMode = false)]
    [ImageUrl("/icons/cms/pages/elected.png")]
    public class BudgetingPage : FoundationPageData, IDisableOPE
    {
    }
}