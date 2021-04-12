using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.MyAccount.Bookmarks.Models;
using Foundation.AspNetCore.Features.MyAccount.Bookmarks.Services;
using Foundation.AspNetCore.Features.MyAccount.Bookmarks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Foundation.AspNetCore.Features.MyAccount.Bookmarks.Controllers
{
    /// <summary>
    /// A page to list all bookmarks belonging to a customer
    /// </summary>
    public class BookmarksController : PageController<BookmarksPage>
    {
        private readonly IBookmarksService _bookmarksService;

        public BookmarksController(IBookmarksService bookmarksService)
        {
            _bookmarksService = bookmarksService;
        }

        //[PageViewTracking]
        public IActionResult Index(BookmarksPage currentPage)
        {
            var model = new BookmarksViewModel(currentPage)
            {
                Bookmarks = _bookmarksService.Get(),
                CurrentContent = currentPage
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Bookmark(Guid contentId)
        {
            _bookmarksService.Add(contentId);
            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult Unbookmark(Guid contentId)
        {
            _bookmarksService.Remove(contentId);
            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult Remove(Guid contentGuid)
        {
            _bookmarksService.Remove(contentGuid);
            return Json(new { Success = true });
        }
    }
}