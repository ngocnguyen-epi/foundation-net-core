using FileHelpers;
using Foundation.AspNetCore.Features.Shared.Interfaces;
using System.IO;

namespace Foundation.AspNetCore.Features.Shared.Services
{
    public class FileHelperService : IFileHelperService
    {
        public T[] GetImportData<T>(Stream file) where T : class
        {
            var reader = new StreamReader(file);

            var fileEngine = new FileHelperEngine(typeof(T));
            fileEngine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;

            return fileEngine.ReadStream(reader, int.MaxValue) as T[];
        }
    }
}