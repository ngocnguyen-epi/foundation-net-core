using Microsoft.AspNetCore.Mvc.Filters;

namespace Foundation.AspNetCore.Infrastructure.Attributes
{
    public class OnlyAnonymousAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.HttpContext.Response.Redirect("/");
            }
        }
    }
}