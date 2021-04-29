using Foundation.AspNetCore.Features.Social.Moderation.Interfaces;
using Foundation.AspNetCore.Features.Social.Moderation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Foundation.AspNetCore.Features.Social.Moderation.Controllers
{
    public class ModerationController : Controller
    {
        private readonly ICommentManagerService _commentManagerService;

        public ModerationController(ICommentManagerService commentManagerService) => _commentManagerService = commentManagerService;

        //[MenuItem("/global/extensions/commentsmanager", TextResourceKey = "/Shared/CommentsManager", SortIndex = 400)]
        [HttpGet]
        public ActionResult Index()
        {
            var model = new ModerationViewModel
            {
                Comments = _commentManagerService.Get(1, 100, out var total).ToList(),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Approve(string id)
        {
            _commentManagerService.Approve(id);

            return new ContentResult
            {
                Content = "Approve successfully.",
            };
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            _commentManagerService.Delete(id);

            return new ContentResult
            {
                Content = "Delete successfully.",
            };
        }
    }
}
