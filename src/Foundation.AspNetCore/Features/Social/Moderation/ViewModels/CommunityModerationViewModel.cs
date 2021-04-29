using Foundation.AspNetCore.Features.Social.Groups.Models;
using Foundation.AspNetCore.Features.Social.Moderation.Models;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Social.Moderation.ViewModels
{
    public class CommunityModerationViewModel
    {
        public CommunityModerationViewModel()
        {
            Workflows = new List<CommunityMembershipWorkflow>();
            Items = new List<CommunityMembershipRequest>();
        }

        public IEnumerable<CommunityMembershipWorkflow> Workflows { get; set; }
        public CommunityMembershipWorkflow SelectedWorkflow { get; set; }
        public IEnumerable<CommunityMembershipRequest> Items { get; set; }
    }
}