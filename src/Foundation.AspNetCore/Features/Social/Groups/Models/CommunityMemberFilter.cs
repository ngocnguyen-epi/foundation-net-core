namespace Foundation.AspNetCore.Features.Social.Groups.Models
{
    public class CommunityMemberFilter
    {
        public string CommunityId { get; set; }

        public int PageSize { get; set; }

        public string UserId { get; set; }
    }
}