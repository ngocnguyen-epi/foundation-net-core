using EPiServer.Core;
using Foundation.AspNetCore.Features.CmsPages.Home;


namespace Foundation.AspNetCore.Features.Header
{
    public interface IHeaderViewModelFactory
    {
        HeaderViewModel CreateHeaderViewModel(IContent content, HomePage homePage);
        //HeaderLogoViewModel CreateHeaderLogoViewModel();
        void AddMyAccountMenu(HomePage homePage, HeaderViewModel viewModel);
    }
}
