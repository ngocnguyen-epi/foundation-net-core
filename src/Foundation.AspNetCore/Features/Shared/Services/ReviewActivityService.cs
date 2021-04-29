using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Features.Social.ActivityStreams.Models;

namespace Foundation.AspNetCore.Features.Shared.Services
{
    public class ReviewActivityService : IReviewActivityService
    {
        private readonly IActivityService _activityService;

        public ReviewActivityService(IActivityService activityService) => _activityService = activityService;

        public void Add(string actor, string target, ReviewActivity activity)
        {
            // Instantiate a reference for the contributor
            var contributor = Reference.Create($"visitor://{actor}");

            // Instantiate a reference for the product
            var product = Reference.Create($"product://{target}");

            _activityService.Add(new Activity(contributor, product), activity);
        }
    }
}