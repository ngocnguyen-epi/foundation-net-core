using EPiServer.Shell.ObjectEditing;
using Mediachase.Commerce.Customers;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Features.Shared.SelectionFactories
{
    public class OrganizationSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var organizationsList = CustomerContext.Current.GetOrganizations();

            return organizationsList.Select(organization => new SelectItem
            {
                Value = organization.Name,
                Text = organization.Name
            }).ToList();
        }
    }
}