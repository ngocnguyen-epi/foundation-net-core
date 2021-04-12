using Foundation.AspNetCore.Cms;
using Foundation.AspNetCore.Features.CmsPages.People.PersonItemPage.Models;
using Foundation.AspNetCore.Features.CmsPages.People.PersonListPage.Models;
using Foundation.AspNetCore.Features.Shared;
using System;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CmsPages.People.PersonListPage.ViewModels
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