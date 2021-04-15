using System.IO;

namespace Foundation.AspNetCore.Features.Shared.Interfaces
{
    public interface IFileHelperService
    {
        T[] GetImportData<T>(Stream file) where T : class;
    }
}