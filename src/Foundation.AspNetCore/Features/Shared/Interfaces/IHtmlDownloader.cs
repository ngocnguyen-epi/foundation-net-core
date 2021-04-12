using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Shared.Interfaces
{
    public interface IHtmlDownloader
    {
        Task<string> Download(string baseUrl, string relativeUrl);
    }
}