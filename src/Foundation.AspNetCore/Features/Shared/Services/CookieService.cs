using Foundation.AspNetCore.Features.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Web;

namespace Foundation.Cms
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual string Get(string key)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return null;
            }

            return _httpContextAccessor.HttpContext.Request.Cookies[key];
        }

        public virtual void Set(string key, string value, bool sessionCookie = false)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return;
            }

            var httpCookie = new CookieOptions()
            {
                HttpOnly = true,
                Secure = _httpContextAccessor.HttpContext?.Request.IsHttps ?? false
            };

            if (!sessionCookie)
            {
                httpCookie.Expires = DateTime.Now.AddYears(1);
            }

            Set(_httpContextAccessor.HttpContext.Response.Cookies, key, value, httpCookie);
        }

        public virtual void Remove(string cookie)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return;
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
        }

        private void Set(IResponseCookies cookieCollection, string key, string value, CookieOptions options) => cookieCollection.Append(key, value, options);
    }
}