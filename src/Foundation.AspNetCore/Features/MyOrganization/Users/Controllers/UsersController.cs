using EPiServer;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Core;
using EPiServer.Framework.Localization;
using EPiServer.Globalization;
using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.MyAccount.ResetPassword.Models;
using Foundation.AspNetCore.Features.MyOrganization.SubOrganization.Models;
using Foundation.AspNetCore.Features.MyOrganization.Users.Models;
using Foundation.AspNetCore.Features.MyOrganization.Users.ViewModels;
using Foundation.AspNetCore.Features.Settings;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Organization.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Organization.Models;
using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Infrastructure;
using Foundation.AspNetCore.Infrastructure.Attributes;
using Foundation.AspNetCore.Infrastructure.Identity;
using Foundation.Cms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foundation.AspNetCore.Features.MyOrganization.Users.Controllers
{
    [Authorize]
    public class UsersController : PageController<UsersPage>
    {
        private readonly ICustomerService _customerService;
        private readonly IOrganizationService _organizationService;
        private readonly IContentLoader _contentLoader;
        private readonly IMailService _mailService;
        private readonly ApplicationUserManager<SiteUser> _userManager;
        private readonly ApplicationSignInManager<SiteUser> _signInManager;
        private readonly LocalizationService _localizationService;
        private readonly ISearchService _searchService;
        private readonly CookieService _cookieService;
        private readonly ISettingsService _settingsService;

        public UsersController(
            ICustomerService customerService,
            IOrganizationService organizationService,
            ApplicationUserManager<SiteUser> userManager,
            ApplicationSignInManager<SiteUser> signinManager,
            IContentLoader contentLoader,
            IMailService mailService,
            LocalizationService localizationService,
            ISearchService searchService,
            CookieService cookieService,
            ISettingsService settingsService)
        {
            _customerService = customerService;
            _organizationService = organizationService;
            _userManager = userManager;
            _signInManager = signinManager;
            _contentLoader = contentLoader;
            _mailService = mailService;
            _localizationService = localizationService;
            _searchService = searchService;
            _cookieService = cookieService;
            _settingsService = settingsService;
        }

        [NavigationAuthorize("Admin")]
        public IActionResult Index(UsersPage currentPage)
        {
            if (TempData["ImpersonateFail"] != null)
            {
                ViewBag.Impersonate = (bool)TempData["ImpersonateFail"];
            }

            var organization = _organizationService.GetCurrentFoundationOrganization();
            var currentOrganization = organization;
            var currentOrganizationContext = _cookieService.Get(Constant.Fields.SelectedOrganization);
            if (currentOrganizationContext != null)
            {
                currentOrganization = _organizationService.GetFoundationOrganizationById(currentOrganizationContext);
            }

            var viewModel = new UsersPageViewModel
            {
                CurrentContent = currentPage,
                Users = _customerService.GetContactsForOrganization(currentOrganization),
                Organizations = organization?.SubOrganizations ?? new List<FoundationOrganization>()
            };

            if (currentOrganization.SubOrganizations.Any())
            {
                foreach (var subOrg in currentOrganization.SubOrganizations)
                {
                    var contacts = _customerService.GetContactsForOrganization(subOrg);
                    viewModel.Users.AddRange(contacts);
                }
            }

            return View(viewModel);
        }

        [NavigationAuthorize("Admin")]
        public IActionResult AddUser(UsersPage currentPage)
        {
            var organization = _organizationService.GetCurrentFoundationOrganization();
            var viewModel = new UsersPageViewModel
            {
                CurrentContent = currentPage,
                Contact = FoundationContact.New(),
                Organizations = organization?.SubOrganizations ?? new List<FoundationOrganization>()
            };
            return View(viewModel);
        }

        [NavigationAuthorize("Admin")]
        public IActionResult EditUser(UsersPage currentPage, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            var organization = _organizationService.GetCurrentFoundationOrganization();
            var contact = _customerService.GetContactById(id);

            var viewModel = new UsersPageViewModel
            {
                CurrentContent = currentPage,
                Contact = contact,
                Organizations = organization?.SubOrganizations ?? new List<FoundationOrganization>(),
                SubOrganization =
                    contact.B2BUserRole != B2BUserRoles.Admin
                        ? _organizationService.GetSubFoundationOrganizationById(contact.FoundationOrganization.OrganizationId.ToString())
                        : new SubFoundationOrganizationModel(organization)
            };
            return View(viewModel);
        }

        [NavigationAuthorize("Admin")]
        public IActionResult RemoveUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            _customerService.RemoveContactFromOrganization(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowDBWrite]
        [NavigationAuthorize("Admin")]
        public IActionResult UpdateUser(UsersPageViewModel viewModel)
        {
            _customerService.EditContact(viewModel.Contact);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowDBWrite]
        [NavigationAuthorize("Admin")]
        public async Task<IActionResult> AddUser(UsersPageViewModel viewModel)
        {
            var user = await _userManager.FindByEmailAsync(viewModel.Contact.Email);
            if (user != null)
            {
                var contact = _customerService.GetContactByEmail(user.Email);
                var organization = _organizationService.GetCurrentFoundationOrganization();
                if (_customerService.HasOrganization(contact.ContactId.ToString()))
                {
                    viewModel.Contact.ShowOrganizationError = true;
                    viewModel.Organizations = organization.SubOrganizations ?? new List<FoundationOrganization>();
                    return View(viewModel);
                }

                var organizationId = organization.OrganizationId.ToString();
                var currentOrganizationContext = _cookieService.Get(Constant.Fields.SelectedOrganization);
                if (currentOrganizationContext != null)
                {
                    organizationId = currentOrganizationContext;
                }

                _customerService.AddContactToOrganization(contact, organizationId);
                _customerService.UpdateContact(contact.ContactId.ToString(), viewModel.Contact.UserRole, viewModel.Contact.UserLocationId);
            }
            else
            {
                await SaveUser(viewModel);
            }

            return RedirectToAction("Index");
        }

        [NavigationAuthorize("Admin")]
        public JsonResult GetUsers(string query)
        {
            var data = _searchService.SearchUsers(query);
            return Json(data);
        }

        public JsonResult GetAddresses(string id)
        {
            var organization = _organizationService.GetSubOrganizationById(id);
            var addresses = organization.Locations;

            return Json(addresses);
        }

        [NavigationAuthorize("Admin")]
        public async Task<IActionResult> ImpersonateUser(string email)
        {
            var success = false;
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                _cookieService.Set(Constant.Cookies.B2BImpersonatingAdmin, User.Identity.Name, true);
                await _signInManager.SignInAsync(user, false);
                success = true;
            }

            if (success)
            {
                return Redirect("/");
            }
            else
            {
                TempData["ImpersonateFail"] = false;
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> BackAsAdmin()
        {
            var adminUsername = _cookieService.Get(Constant.Cookies.B2BImpersonatingAdmin);
            if (!string.IsNullOrEmpty(adminUsername))
            {
                var adminUser = await _userManager.FindByEmailAsync(adminUsername);
                if (adminUser != null)
                {
                    await _signInManager.SignInAsync(adminUser, false);
                }

                _cookieService.Remove(Constant.Cookies.B2BImpersonatingAdmin);
            }
            return Redirect(Request.Headers["UrlReferrer"].ToString() ?? "/");
        }

        private async Task SaveUser(UsersPageViewModel viewModel)
        {
            var contactUser = new SiteUser
            {
                UserName = viewModel.Contact.Email,
                Email = viewModel.Contact.Email,
                Password = "password",
                FirstName = viewModel.Contact.FirstName,
                LastName = viewModel.Contact.LastName,
                RegistrationSource = "Registration page"
            };

            await _userManager.CreateAsync(contactUser);

            _customerService.CreateContact(viewModel.Contact, contactUser.Id);

            var user = await _userManager.FindByNameAsync(viewModel.Contact.Email);
            if (user != null)
            {
                var referencePages = _settingsService.GetSiteSettings<ReferencePageSettings>();
                if (ContentReference.IsNullOrEmpty(referencePages?.ResetPasswordMail))
                {
                    return;
                }
                var body = await _mailService.GetHtmlBodyForMail(referencePages.ResetPasswordMail, new NameValueCollection(), ContentLanguage.PreferredCulture.TwoLetterISOLanguageName);
                var mailPage = _contentLoader.Get<MailBasePage>(referencePages.ResetPasswordMail);
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var url = Url.Action("ResetPassword", "ResetPassword", new { userId = user.Id, code = HttpUtility.UrlEncode(code), language = ContentLanguage.PreferredCulture.TwoLetterISOLanguageName }, Request.Scheme);

                body = body.Replace("[MailUrl]",
                    string.Format("{0}<a href=\"{1}\">{2}</a>",
                        _localizationService.GetString("/ResetPassword/Mail/Text"),
                        url,
                        _localizationService.GetString("/ResetPassword/Mail/Link")));

                _mailService.Send(mailPage.Subject, body, user.Email);
            }
        }
    }
}