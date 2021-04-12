using EPiServer.Web;
using Microsoft.AspNetCore.Http;

namespace Foundation.AspNetCore.Business.Channels
{
    public class PartialViewChannel : DisplayChannel
    {
        public const string PartialViewDisplayChannelName = "Partial View Preview";

        public override string ChannelName => PartialViewDisplayChannelName;

        public override bool IsActive(HttpContext context) => false;
    }
}
