using Foundation.AspNetCore.Features.Social.Adapters;

namespace Foundation.AspNetCore.Features.Social.ActivityStreams.Models
{
    public abstract class CommunityActivity : ICommunityActivity
    {
        public abstract void Accept(ICommunityActivityAdapter adapter);
    }
}