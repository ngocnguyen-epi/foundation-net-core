using EPiServer.Core;
using Foundation.AspNetCore.Features.CmsPages.Home;

namespace Foundation.AspNetCore.Features.Shared
{
    public interface IContentViewModel<out TContent> where TContent : IContent
    {
        TContent CurrentContent { get; }
        HomePage StartPage { get; }
        //HtmlString SchemaMarkup { get; }
    }
}