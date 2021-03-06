using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Organization.Models;
using System;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Organization.Interfaces
{
    public interface IOrganizationService
    {
        OrganizationModel GetOrganizationModel(FoundationOrganization organization = null);
        OrganizationModel GetOrganizationModel(Guid id);
        List<OrganizationModel> GetOrganizationModels();
        void CreateOrganization(OrganizationModel organizationInfo);
        void UpdateOrganization(OrganizationModel organizationInfo);
        void CreateSubOrganization(SubOrganizationModel newSubOrganization);
        SubOrganizationModel GetSubOrganizationById(string subOrganizationId);
        SubFoundationOrganizationModel GetSubFoundationOrganizationById(string subOrganizationId);
        void UpdateSubOrganization(SubOrganizationModel subOrganizationModel);
        string GetUserCurrentOrganizationLocation();
        FoundationOrganization GetCurrentFoundationOrganization();
        FoundationOrganization GetFoundationOrganizationById(string organizationId);
        List<FoundationOrganization> GetOrganizations();
    }
}