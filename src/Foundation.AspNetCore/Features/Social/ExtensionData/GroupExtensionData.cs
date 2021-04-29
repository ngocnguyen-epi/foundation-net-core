namespace Foundation.AspNetCore.Features.Social.ExtensionData
{
    public class GroupExtensionData
    {
        public GroupExtensionData(string pageLink) => PageLink = pageLink;

        public string PageLink { get; set; }
    }
}