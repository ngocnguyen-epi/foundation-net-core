using Foundation.AspNetCore.Features.Shared;

namespace Foundation.Features.CmsPages.People.PersonItemPage
{
    public class PersonItemViewModel : ContentViewModel<PersonPage>
    {
        public PersonItemViewModel(PersonPage currentPage) : base(currentPage) { }
    }
}