using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Features.Shared.SelectionFactories
{
    public class SectorsSelectionFactory : ISelectionFactory
    {
        private static readonly Lazy<ISettingsService> _settingsService = new Lazy<ISettingsService>(() => ServiceLocator.Current.GetInstance<ISettingsService>());

        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var settings = _settingsService.Value.GetSiteSettings<CollectionSettings>();
            return settings?.Sectors?.Select(x => new SelectItem { Value = x.Value, Text = x.Text }) ?? new List<SelectItem>();
        }
    }
}
