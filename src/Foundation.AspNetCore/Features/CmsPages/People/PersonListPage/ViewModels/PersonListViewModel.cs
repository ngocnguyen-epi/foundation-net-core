using Foundation.AspNetCore.Cms;
using Foundation.AspNetCore.Features.Shared;
using Foundation.Features.CmsPages.People.PersonItemPage;
using Foundation.Features.CmsPages.People.PersonListPage;
using System;
using System.Collections.Generic;

namespace Foundation.Features.People.PersonListPage
{
    public class PersonListViewModel : ContentViewModel<PersonList>
    {
        public PersonListViewModel(PersonList currentPage) : base(currentPage) { }
        public IEnumerable<PersonPage> Persons { get; set; }
        public List<SelectionItem> Sectors { get; set; }
        public List<SelectionItem> Locations { get; set; }
        public List<string> Names { get; set; }
    }
}