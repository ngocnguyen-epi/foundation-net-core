using EPiServer.Shell.ObjectEditing;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Business.SelectionFactories
{
    public class VirtualVariantTypeSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                new SelectItem { Text = "None", Value = "None" },
                new SelectItem { Text = "Key", Value = "Key" },
                new SelectItem { Text = "File", Value = "File" },
                new SelectItem { Text = "Elevated Role", Value = "ElevatedRole" }
            };
        }
    }
}