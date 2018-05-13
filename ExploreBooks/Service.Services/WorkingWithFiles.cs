using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Service.Interfaces;

namespace Service.Services
{
    public class WorkingWithFiles : IWorkingWithFiles
    {
        private readonly IHostingEnvironment _env;
        private readonly string _imagePath;

        public WorkingWithFiles(IHostingEnvironment env)
        {
            _env = env;
            _imagePath = "images\\";
        }

        public async Task CreateFile(string folder, IFormFile image)
        {
            DeleteFolder(folder);
            var path = Path.Combine(_env.WebRootPath, _imagePath + folder);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var fileStream = new FileStream(Path.Combine(path, image.FileName), FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
        }

        public void DeleteFolder(string folder)
        {
            var searchedPath = Path.Combine(_env.WebRootPath, _imagePath + folder);

            if (Directory.Exists(searchedPath))
            {
                Directory.Delete(searchedPath, true);
            }
        }

        public void DeleteFileForGivenId(string folder, Guid value, string fileName)
        {
            var searchedPath = Path.Combine(_env.WebRootPath, _imagePath + folder + "\\" + value + "\\" + fileName);

            if (File.Exists(searchedPath))
            {
                File.Delete(searchedPath);
            }
        }

        public string GetPath(string folder, Guid value)
        {
            return Path.Combine(_env.WebRootPath, _imagePath + folder + "\\" + value);
        }

        public void CopyFile(string folder, Guid value)
        {
            var path = Path.Combine(_env.WebRootPath, _imagePath + "default");
            
            foreach (var file in Directory.GetFiles(path))
            {
                if (Path.GetFileNameWithoutExtension(file) == folder)
                {
                    var targetPath = Path.Combine(_env.WebRootPath, _imagePath + folder + "\\" + value);

                    if (!Directory.Exists(targetPath))
                    {
                        Directory.CreateDirectory(targetPath);
                    }

                    targetPath = Path.Combine(targetPath, Path.GetFileName(file));
                    File.Copy(file, targetPath);
                }
            }
        }
    }
}
