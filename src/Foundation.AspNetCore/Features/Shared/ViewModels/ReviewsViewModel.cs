using Foundation.AspNetCore.Features.Social.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.ViewModels
{
    public class ReviewsViewModel
    {
        public ReviewsViewModel()
        {
            Reviews = new List<ReviewViewModel>();
            Statistics = new ReviewStatisticsViewModel();
        }

        public ReviewStatisticsViewModel Statistics { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; }
    }
}