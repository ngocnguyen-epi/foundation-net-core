using EPiServer.Core;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Http;

namespace Foundation.AspNetCore.Cms.Extensions
{
    public static class UrlResolverExtensions
    {
        public static string GetUrl(this UrlResolver urlResolver, HttpRequest request, ContentReference contentLink,
            string language)
        {
            if (!ContentReference.IsNullOrEmpty(contentLink))
            {
                return urlResolver.GetUrl(contentLink, language);
            }

            return request.Path == null ? "/" : request.Path;
        }
    }
}
