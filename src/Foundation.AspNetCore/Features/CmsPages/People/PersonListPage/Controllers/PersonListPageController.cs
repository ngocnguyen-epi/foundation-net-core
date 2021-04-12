using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Cms;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Extensions;
using Foundation.AspNetCore.Features.CmsPages.People.PersonItemPage.Models;
using Foundation.AspNetCore.Features.CmsPages.People.PersonListPage.Models;
using Foundation.AspNetCore.Features.CmsPages.People.PersonListPage.ViewModels;
using Foundation.AspNetCore.Features.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Features.CmsPages.People.PersonListPage.Controllers
{
    public class PersonListPageController : PageController<PersonList>
    {
        private readonly ISettingsService _settingsService;

        public PersonListPageController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public ActionResult Index(PersonList currentPage)
        {

            // TODO: Wait for Find to support net core
            var persons = FindPersonPages().ToList().Select(x => x as PersonPage);
            //
            var queryString = Request.QueryString;

            string _name = HttpContext.Request.Query["name"];
            if (!string.IsNullOrEmpty(_name))
            {
                persons = persons.Where(person => person.Name.Contains(_name));
            }

            string _sector = HttpContext.Request.Query["sector"];
            if (!string.IsNullOrEmpty(_sector))
            {
                persons = persons.Where(person => person.Sector.Contains(_sector));
            }

            string _location = HttpContext.Request.Query["location"];
            if (!string.IsNullOrEmpty(_location))
            {
                persons = persons.Where(person => person.Location.Contains(_location));
            }
            //
            var collectionSettingPage = _settingsService.GetSiteSettings<CollectionSettings>();
            var sectorsList = collectionSettingPage?.Sectors?.OrderBy(x => x.Text).ToList() ?? new List<SelectionItem>();
            var locationsList = collectionSettingPage?.Locations?.OrderBy(x => x.Text).ToList() ?? new List<SelectionItem>();

            var model = new PersonListViewModel(currentPage)
            {
                Persons = persons,
                Sectors = sectorsList,
                Locations = locationsList,
            };

            return View(model);
        }

        public List<string> GetNames(IEnumerable<PageData> persons)
        {
            var lstNames = new List<string>();
            foreach (var person in persons)
            {
                lstNames.Add(person.Name);
            }
            return lstNames.Distinct().OrderBy(x => x).ToList();
        }

        private IEnumerable<PageData> FindPersonPages()
        {
            var startPage = (ContentReference)ContentReference.StartPage;
            var personPageTypeId = ServiceLocator.Current.GetInstance<IContentTypeRepository>().Load<PersonPage>().ID;
            var pages = startPage.FindPagesByPageType(true, personPageTypeId);
            return pages;
        }
    }
}
