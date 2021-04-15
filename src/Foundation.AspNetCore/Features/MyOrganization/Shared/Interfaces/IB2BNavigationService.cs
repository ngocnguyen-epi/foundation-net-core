using EPiServer.SpecializedProperties;

namespace Foundation.AspNetCore.Features.MyOrganization.Shared.Interfaces
{
    public interface IB2BNavigationService
    {
        LinkItemCollection FilterB2BNavigationForCurrentUser(LinkItemCollection b2BLinks);
    }
}