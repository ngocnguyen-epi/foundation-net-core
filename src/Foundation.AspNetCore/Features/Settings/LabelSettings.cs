﻿using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Cms.Settings;
using System.ComponentModel.DataAnnotations;

namespace Foundation.AspNetCore.Features.Settings
{
    [SettingsContentType(DisplayName = "Label Settings",
        GUID = "c17375a6-4a01-402b-8c7f-18257e944527",
        SettingsName = "Site Labels")]
    public class LabelSettings : SettingsBase
    {
        [CultureSpecific]
        [Display(Name = "My account", GroupName = Global.TabNames.SiteLabels, Order = 10)]
        public virtual string MyAccountLabel { get; set; }

        [CultureSpecific]
        [Display(Name = "Search", GroupName = Global.TabNames.SiteLabels, Order = 30)]
        public virtual string SearchLabel { get; set; }
    }
}