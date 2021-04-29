using Foundation.AspNetCore.Features.Social.Adapters;

namespace Foundation.AspNetCore.Features.Social.ActivityStreams.Models
{
    public class PageRatingActivity : CommunityActivity
    {
        public int Value { get; set; }

        public override void Accept(ICommunityActivityAdapter adapter) => adapter.Visit(this);
    }
}