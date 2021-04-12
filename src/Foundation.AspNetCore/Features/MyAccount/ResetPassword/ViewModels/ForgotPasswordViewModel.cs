using Foundation.AspNetCore.Features.MyAccount.ResetPassword.Models;
using Foundation.AspNetCore.Features.Shared;

namespace Foundation.AspNetCore.Features.MyAccount.ResetPassword.ViewModels
{
    public class ForgotPasswordViewModel : ContentViewModel<ResetPasswordPage>
    {
        public ForgotPasswordViewModel(ResetPasswordPage resetPasswordPage) : base(resetPasswordPage)
        {
        }

        public ForgotPasswordViewModel() { }

        //[LocalizedDisplay("/ResetPassword/Form/Label/Email")]
        //[LocalizedRequired("/ResetPassword/Form/Empty/Email")]
        //[LocalizedEmail("/ResetPassword/Form/Error/InvalidEmail")]
        public string Email { get; set; }
    }
}