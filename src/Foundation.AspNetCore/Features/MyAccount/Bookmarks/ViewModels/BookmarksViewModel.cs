using Foundation.AspNetCore.Features.MyAccount.Bookmarks.Models;
using Foundation.AspNetCore.Features.Shared;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyAccount.Bookmarks.ViewModels
{
    public class BookmarksViewModel : ContentViewModel<BookmarksPage>
    {
        public List<BookmarkModel> Bookmarks { get; set; }
        public BookmarksViewModel(BookmarksPage currentPage) : base(currentPage) { }
        public BookmarksViewModel() : base() { }
    }
}
