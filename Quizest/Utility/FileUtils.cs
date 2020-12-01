using System;
using System.IO;
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

                output = @$"data:image/{extension};base64," + Convert.ToBase64String(bytes);
            }

            return output;
        }

        public static string GetContent(string path) => GetContentAsync(path).Result;
    }
}
