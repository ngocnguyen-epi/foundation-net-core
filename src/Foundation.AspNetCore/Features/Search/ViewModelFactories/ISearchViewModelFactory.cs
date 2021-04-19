using EPiServer.Core;
using Foundation.AspNetCore.Features.Search.ViewModels;

namespace Foundation.AspNetCore.Features.Search.ViewModelFactories
{
    public interface ISearchViewModelFactory
    {
        SearchViewModel<TContent> Create<TContent>(TContent currentContent, string selectedFacets,
            int catlogId, FilterOptionViewModel filterOption)
            where TContent : IContent;
    }
}
