using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;
using Foundation.AspNetCore.Infrastructure;
using Foundation.AspNetCore.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces
{
    public interface ICustomerService
    {
        Guid CurrentContactId { get; }
        void CreateContact(FoundationContact contact, string contactId);
        void EditContact(FoundationContact model);
        void RemoveContactFromOrganization(string id);
        bool CanSeeOrganizationNav();
        void AddContactToOrganization(FoundationContact contact, string organizationId = null);
        void UpdateContact(string contactId, string userRole, string location = null);
        FoundationContact GetContactByEmail(string email);
        FoundationContact GetCurrentContact();
        FoundationContact GetContactById(string contactId);
        List<FoundationContact> GetContactsForOrganization(FoundationOrganization organization = null);
        void AddContactToOrganization(FoundationOrganization organization, FoundationContact contact, B2BUserRoles userRole);
        List<FoundationContact> GetContacts();
        Task<SiteUser> GetSiteUserAsync(string email);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<IdentityContactResult> CreateUser(SiteUser user);
        Task SignOutAsync();
        bool HasOrganization(string contactId);
        ContactViewModel GetCurrentContactViewModel();
    }
}