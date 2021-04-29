namespace Foundation.AspNetCore.Features.Social.Services.Comments.Models
{
    public class PageCommentFilter
    {
        public string Author { get; set; }

        public string Target { get; set; }

        public int PageSize { get; set; }

        public int PageOffset { get; set; }
    }
}