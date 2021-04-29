namespace Foundation.AspNetCore.Features.Social.ActivityStreams.Models
{
    public class CommunityFeedFilter
    {
        public string Subscriber { get; set; }

        public int PageSize { get; set; }

        public int PageOffset { get; set; }
    }
}