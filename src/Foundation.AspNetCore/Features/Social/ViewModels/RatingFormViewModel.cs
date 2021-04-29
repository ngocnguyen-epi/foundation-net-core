using EPiServer.Core;

namespace Foundation.AspNetCore.Features.Social.ViewModels
{
    public class RatingFormViewModel
    {
        public int? SubmittedRating { get; set; }

        public bool SendActivity { get; set; }

        public PageReference CurrentLink { get; set; }
    }
}