using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TechMart.Presentation.Modules.Shared.Interfaces;

namespace TechMart.Presentation.Modules.Shared.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;

        public FileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string?> SaveFileAsync(IFormFile? file, string folderPath, string[] allowedExtensions, string? oldFilePath = null)
        {
            if (file == null || file.Length == 0)
            {
                return oldFilePath;
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                return oldFilePath; 
            }

            var physicalPath = Path.Combine(_env.WebRootPath, folderPath);
            Directory.CreateDirectory(physicalPath);

            var fileName = $"{Guid.NewGuid():N}{extension}";
            var fullPath = Path.Combine(physicalPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(oldFilePath))
            {
                DeleteFile(oldFilePath);
            }

            return $"/{folderPath.Replace("\\", "/")}/{fileName}";
        }

        public void DeleteFile(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            var fullPath = Path.Combine(_env.WebRootPath, filePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
