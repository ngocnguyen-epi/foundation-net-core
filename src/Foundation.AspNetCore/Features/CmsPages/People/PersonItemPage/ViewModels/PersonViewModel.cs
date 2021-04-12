using Foundation.AspNetCore.Features.CmsPages.People.PersonItemPage.Models;
using Foundation.AspNetCore.Features.Shared;

namespace Foundation.AspNetCore.Features.CmsPages.People.PersonItemPage.ViewModels
{
    public class PersonItemViewModel : ContentViewModel<PersonPage>
    {
        public PersonItemViewModel(PersonPage currentPage) : base(currentPage) { }
    }
}