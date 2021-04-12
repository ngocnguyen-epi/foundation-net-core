using EPiServer.DataAbstraction;
using Foundation.AspNetCore.Features.Shared;
using System.Linq;

namespace Foundation.AspNetCore.Features.CmsPages.StandardPage.ViewModels
{
    public class StandardPageViewModel : ContentViewModel<Models.StandardPage>
    {
        public string CategoryName { get; set; }

        public StandardPageViewModel(Models.StandardPage currentPage) : base(currentPage)
        {
        }

        public static StandardPageViewModel Create(Models.StandardPage currentPage, CategoryRepository categoryRepository)
        {
            var model = new StandardPageViewModel(currentPage);
            if (currentPage.Category.Any())
            {
                model.CategoryName = categoryRepository.Get(currentPage.Category.FirstOrDefault()).Description;
            }
            return model;
        }
    }
}
