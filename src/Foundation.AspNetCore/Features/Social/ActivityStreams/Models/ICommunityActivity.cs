using Foundation.AspNetCore.Features.Social.Adapters;

namespace Foundation.AspNetCore.Features.Social.ActivityStreams.Models
{
    public interface ICommunityActivity
    {
        void Accept(ICommunityActivityAdapter adapter);
    }
}