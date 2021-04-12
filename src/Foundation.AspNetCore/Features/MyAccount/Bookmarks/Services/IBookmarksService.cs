using Foundation.AspNetCore.Features.MyAccount.Bookmarks.Models;
using System;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyAccount.Bookmarks.Services
{
    public interface IBookmarksService
    {
        void Add(Guid contentGuid);
        void Remove(Guid contentGuid);
        List<BookmarkModel> Get();
    }
}
