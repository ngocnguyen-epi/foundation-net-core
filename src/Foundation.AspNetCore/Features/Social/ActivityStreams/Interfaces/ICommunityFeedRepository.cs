using Foundation.AspNetCore.Features.Social.ActivityStreams.Models;
using Foundation.AspNetCore.Features.Social.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Social.ActivityStreams.Interfaces
{
    /// <summary>
    ///     The ICommunityFeedRepository interface defines the operations for accessing feeds of community activities.
    /// </summary>
    public interface ICommunityFeedRepository
    {
        /// <summary>
        ///     Retrieves feed items based on the specified filter.
        /// </summary>
        /// <param name="filter">A feed item filter</param>
        /// <returns>A list of feed items.</returns>
        IEnumerable<CommunityFeedItemViewModel> Get(CommunityFeedFilter filter);
    }
}