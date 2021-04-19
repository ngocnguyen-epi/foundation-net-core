using Foundation.AspNetCore.Features.Search.ViewModels;

namespace Foundation.AspNetCore.Features.Search.Services
{
    public interface ISearchService
    {
        ContentSearchViewModel SearchContent(FilterOptionViewModel filterOptions);
    }
}
