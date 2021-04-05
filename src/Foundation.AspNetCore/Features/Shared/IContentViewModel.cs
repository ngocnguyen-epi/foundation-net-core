using EPiServer.Core;
using Foundation.AspNetCore.Features.Home;

namespace Foundation.AspNetCore.Features.Shared
{
    public interface IContentViewModel<out TContent> where TContent : IContent
    {
        TContent CurrentContent { get; }
        HomePage StartPage { get; }
        //HtmlString SchemaMarkup { get; }
    }
}