using Foundation.AspNetCore.Features.Social.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Social.Moderation.Interfaces
{
    public interface ICommentManagerService
    {
        IEnumerable<ReviewViewModel> Get(int page, int limit, out long total);
        void Delete(string id);
        //Comment Approve(string id);
        void Approve(string id);
    }
}