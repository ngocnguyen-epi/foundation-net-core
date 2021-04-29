namespace Foundation.AspNetCore.Features.Social.ActivityStreams.Models
{
    public class ReviewActivity
    {
        public int Rating { get; set; }
        public double OverallRating { get; set; }
        public string Contributor { get; set; }
        public string Product { get; set; }
    }
}