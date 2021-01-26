using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Utility
{
    public static class FileUtils
    {
        private static string basePath;

        public static string BasePath 
        {
            get => basePath;
            
            set 
            {
                string parent = Directory.GetParent(value).FullName;

                basePath = new Uri(parent).LocalPath;
            } 
        }

        public static void InitFileStorageDirectories()
        {
            string avatarDirPath = BasePath + DirType.Avatars;

            string previewDirPath = BasePath + DirType.Previews;

            if (!Directory.Exists(avatarDirPath))
            {
                Directory.CreateDirectory(avatarDirPath);
            }

            if (!Directory.Exists(previewDirPath))
            {
                Directory.CreateDirectory(previewDirPath);
            }
        }

        public static async Task<string> GetContentAsync(string path)
        {
            string output = string.Empty;

            if (File.Exists(BasePath + path))
            {
                var bytes = await File.ReadAllBytesAsync(BasePath + path);

                string extension = Path.GetExtension(BasePath + path).Remove(0, 1).ToLower();

                output = Constants.GetContentType(extension) + Convert.ToBase64String(bytes);
            }

            return output;
        }

        public static string GetContent(string path) => GetContentAsync(path).Result;

        public static async Task<string> SaveAsync(string dirType, IFormFile formFile)
        {
            string relative = dirType 
                + RandomGenerator.GenerateHexKey(Constants.FileNameLength) 
                + Path.GetExtension(formFile.FileName);

            using var fileStream = new FileStream(BasePath + relative, FileMode.Create);

            await formFile.CopyToAsync(fileStream);

            return relative;
        }

        public static void Remove(string path)
        {
            path = BasePath + path;

            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static async Task<string> UpdateAsync(string dirType, string oldPath, IFormFile formFile)
        {
            Remove(oldPath);
            return await SaveAsync(dirType, formFile);
        }

        public static bool IsPreviewValid(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            bool extensionIsValid = Constants.AllowedPreviewExtensions
                                    .Where(x => extension.ToLower().Equals("." + x))
                                    .Count() != 0;

            bool contentTypeIsValid = Constants.AllowedPreviewContentTypes
                                      .Where(x => file.ContentType.ToLower().Equals(Constants.ImagePrefix + x))
                                      .Count() != 0;

            return extensionIsValid && contentTypeIsValid && file.Length < Constants.MaxPreviewSize;
        }

    }
}
