namespace Foundation.AspNetCore.Features.Social.Moderation.Models
{
    public class CommunityMembershipWorkflow
    {
        public CommunityMembershipWorkflow(string id, string name, string initialState)
        {
            Id = id;
            Name = name;
            InitialState = initialState;
        }

        public string Id { get; set; }

        public string InitialState { get; set; }

        public string Name { get; set; }
    }
}