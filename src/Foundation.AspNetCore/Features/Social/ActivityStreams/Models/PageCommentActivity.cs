using Foundation.AspNetCore.Features.Social.Adapters;

namespace Foundation.AspNetCore.Features.Social.ActivityStreams.Models
{
    public class PageCommentActivity : CommunityActivity
    {
        public string Body { get; set; }

        public override void Accept(ICommunityActivityAdapter adapter) => adapter.Visit(this);
    }
}