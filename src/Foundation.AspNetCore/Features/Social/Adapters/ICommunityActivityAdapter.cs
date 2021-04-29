using Foundation.AspNetCore.Features.Social.ActivityStreams.Models;
using Foundation.AspNetCore.Features.Social.ViewModels;

namespace Foundation.AspNetCore.Features.Social.Adapters
{
    public interface ICommunityActivityAdapter
    {
        //CommunityFeedItemViewModel Adapt(Composite<FeedItem, CommunityActivity> composite);

        void Visit(CommunityActivity activity);

        void Visit(PageCommentActivity activity);

        void Visit(PageRatingActivity activity);
    }
}