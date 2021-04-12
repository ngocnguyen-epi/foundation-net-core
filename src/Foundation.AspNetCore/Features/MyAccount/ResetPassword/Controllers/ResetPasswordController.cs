﻿using EPiServer;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Core;
using EPiServer.Framework.Localization;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.CmsPages.Home;
using Foundation.AspNetCore.Features.MyAccount.ResetPassword.Models;
using Foundation.AspNetCore.Features.MyAccount.ResetPassword.ViewModels;
using Foundation.AspNetCore.Features.MyAccount.Shared.Controllers;
using Foundation.AspNetCore.Features.Settings;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Foundation.AspNetCore.Features.MyAccount.ResetPassword.Controllers
{
    public class ResetPasswordController : IdentityControllerBase<ResetPasswordPage>
    {
        private readonly IContentLoader _contentLoader;
        private readonly IMailService _mailService;
        private readonly LocalizationService _localizationService;
        private readonly ISettingsService _settingsService;

        public ResetPasswordController(ApplicationSignInManager<SiteUser> signinManager,
            ApplicationUserManager<SiteUser> userManager,
            ICustomerService customerService,
            IContentLoader contentLoader,
            IMailService mailService,
            LocalizationService localizationService,
            ISettingsService settingsService)

            : base(signinManager, userManager, customerService)
        {
            _contentLoader = contentLoader;
            _mailService = mailService;
            _localizationService = localizationService;
            _settingsService = settingsService;
        }

        [AllowAnonymous]
        public IActionResult Index(ResetPasswordPage currentPage)
        {
            var viewModel = new ForgotPasswordViewModel(currentPage);
            return View("ForgotPassword", viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model, string language)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            var referencePages = _settingsService.GetSiteSettings<ReferencePageSettings>();
            //var body = _mailService.GetHtmlBodyForMail(startPage.ResetPasswordMail, new NameValueCollection(), language);
            var mailPage = _contentLoader.Get<MailBasePage>(referencePages.ResetPasswordMail);
            var body = mailPage.MainBody.ToHtmlString();
            var code = await UserManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "ResetPassword", new { userId = user.Id, code = HttpUtility.UrlEncode(code), language }, Request.Scheme);

            body = body.Replace("[MailUrl]",
                string.Format("{0}<a href=\"{1}\">{2}</a>", _localizationService.GetString("/ResetPassword/Mail/Text"), url, _localizationService.GetString("/ResetPassword/Mail/Link"))
            );

            _mailService.Send(mailPage.Subject, body, user.Email);

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            var homePage = _contentLoader.Get<PageData>(ContentReference.StartPage) as HomePage;
            var model = ContentViewModel.Create(homePage);
            return View("ForgotPasswordConfirmation", model);
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordPage currentPage, string code)
        {
            var viewModel = new ResetPasswordViewModel(currentPage) { Code = code };
            return code == null ? View("Error") : View("ResetPassword", viewModel);
        }

        [HttpPost]
        //[AllowDBWrite]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await UserManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(model.Code), model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            AddErrors(result.Errors.Select(x => x.Description));

            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            var homePage = _contentLoader.Get<PageData>(ContentReference.StartPage) as HomePage;
            var model = ContentViewModel.Create(homePage);
            return View("ResetPasswordConfirmation", model);
        }
    }
}