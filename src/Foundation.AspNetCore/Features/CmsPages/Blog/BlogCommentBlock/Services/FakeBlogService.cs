using Foundation.AspNetCore.Features.CmsPages.Blog.BlogCommentBlock.Interfaces;
using Foundation.AspNetCore.Features.CmsPages.Blog.BlogCommentBlock.Models;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CmsPages.Blog.BlogCommentBlock.Services
{
    public class FakeCommentBlogService : IBlogCommentRepository
    {
        public BlogCommentModel Add(BlogCommentModel comment)
        {
            return new BlogCommentModel();
        }

        public IEnumerable<BlogCommentModel> Get(PageCommentFilter filter, out long total)
        {
            total = 0;
            return new List<BlogCommentModel>();
        }
    }

    public class FakePageCommentRepository : IPageCommentRepository
    {
        public PageComment Add(PageComment comment)
        {
            return new PageComment();
        }

        public IEnumerable<PageComment> Get(PageCommentFilter filter)
        {
            return new List<PageComment>();
        }
    }
}
