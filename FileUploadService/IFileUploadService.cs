namespace RemoteEduApp.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string filePath);
    }
}
