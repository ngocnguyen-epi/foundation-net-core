using Microsoft.AspNetCore.Http;

namespace Foundation.AspNetCore.Features.Shared.Interfaces
{
    public interface ICookieService
    {
        string Get(string cookie);

        void Set(string cookie, string value, bool sessionCookie = false);

        void Remove(string cookie);
    }
}