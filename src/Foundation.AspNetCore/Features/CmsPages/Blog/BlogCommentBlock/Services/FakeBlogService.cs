using Foundation.AspNetCore.Features.CmsPages.Blog.BlogCommentBlock.Interfaces;
using Foundation.AspNetCore.Features.CmsPages.Blog.BlogCommentBlock.Models;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CmsPages.Blog.BlogCommentBlock.Services
{
    public class FakeCommentBlogService : IBlogCommentRepository
    {
        public BlogComment Add(BlogComment comment)
        {
            return new BlogComment();
        }

        public IEnumerable<BlogComment> Get(PageCommentFilter filter, out long total)
        {
            total = 0;
            return new List<BlogComment>();
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
