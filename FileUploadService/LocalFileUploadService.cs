
using Microsoft.AspNetCore.Hosting;

namespace RemoteEduApp.Services
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public LocalFileUploadService(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> UploadFileAsync(IFormFile file, string filePath)
        {
            var fullFilePath = String.Concat(_environment.ContentRootPath, "/wwwroot", filePath);

            await Console.Out.WriteLineAsync(fullFilePath);
            using var fileStream = new FileStream(fullFilePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return fullFilePath;
        }
    }
}
