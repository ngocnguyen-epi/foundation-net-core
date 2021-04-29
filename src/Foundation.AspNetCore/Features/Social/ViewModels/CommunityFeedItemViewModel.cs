using System;

namespace Foundation.AspNetCore.Features.Social.ViewModels
{
    public class CommunityFeedItemViewModel
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}