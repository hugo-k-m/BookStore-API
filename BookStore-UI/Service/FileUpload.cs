using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;
using BookStore_UI.Contracts;
using Microsoft.AspNetCore.Hosting;

namespace BookStore_UI.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _env;

        public FileUpload(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void RemoveFile(string picName)
        {
            var path = $"{_env.WebRootPath}\\uploads\\{picName}";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void UploadFile(IFileListEntry file, MemoryStream msFile, string picName)
        {
            try
            {
                var path = $"{_env.WebRootPath}\\uploads\\{picName}";

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    msFile.WriteTo(fs);
                }

            }
            catch
            {

                throw;
            }
        }
    }
}
