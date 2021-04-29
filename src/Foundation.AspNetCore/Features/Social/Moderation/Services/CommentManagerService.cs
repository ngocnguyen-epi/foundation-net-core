using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Features.Social.Moderation.Interfaces;
using Foundation.AspNetCore.Features.Social.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Social.Moderation.Services
{
    public class CommentManagerService : ICommentManagerService
    {
        private readonly ICommentService _commentService;
        private readonly IReviewService _reviewService;

        public CommentManagerService(ICommentService commentService,
            IReviewService reviewService)
        {
            _commentService = commentService;
            _reviewService = reviewService;
        }

        public Comment Approve(string id)
        {
            var commentId = CommentId.Create(id);
            var comment = _commentService.Get(commentId);
            var updatedComment = new Comment(comment.Id, comment.Parent, comment.Author, comment.Body, true);
            return _commentService.Update(updatedComment);
        }

        public void Delete(string id)
        {
            var commentId = CommentId.Create(id);
            _commentService.Remove(commentId);
        }

        public IEnumerable<ReviewViewModel> Get(int page, int limit, out long total) => _reviewService.Get(Visibility.All, page, limit, out total);
    }
}