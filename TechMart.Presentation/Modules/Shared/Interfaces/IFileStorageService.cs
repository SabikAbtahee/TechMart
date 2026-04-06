using Microsoft.AspNetCore.Http;

namespace TechMart.Presentation.Modules.Shared.Interfaces
{
    public interface IFileStorageService
    {
        Task<string?> SaveFileAsync(IFormFile? file, string folderPath, string[] allowedExtensions, string? oldFilePath = null);

        void DeleteFile(string? filePath);
    }
}
