using Foundation.AspNetCore.Features.Social.ActivityStreams.Models;

namespace Foundation.AspNetCore.Features.Social.ActivityStreams.Interfaces
{
    /// <summary>
    ///     This interface defines the operations used to add activities pretaining to community actions.
    /// </summary>
    public interface ICommunityActivityRepository
    {
        /// <summary>
        ///     Adds an activity.
        /// </summary>
        /// <param name="actor">the actor who initiated the activity</param>
        /// <param name="target">the target of the activity</param>
        /// <param name="activity">an instance of CommunityActivity</param>
        void Add(string actor, string target, CommunityActivity activity);
    }
}