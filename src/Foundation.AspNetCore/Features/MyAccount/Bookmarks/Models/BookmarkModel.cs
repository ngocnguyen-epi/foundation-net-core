using EPiServer.Core;
using System;

namespace Foundation.AspNetCore.Features.MyAccount.Bookmarks.Models
{
    public class BookmarkModel
    {
        public ContentReference ContentLink { get; set; }
        public Guid ContentGuid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
