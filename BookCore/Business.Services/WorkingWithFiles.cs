using System;
using System.IO;
using System.Threading.Tasks;
using Data.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Business.Services
{
    public class WorkingWithFiles : IWorkingWithFiles
    {
        private readonly IHostingEnvironment _env;

        public WorkingWithFiles(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task CreateFile(string folder, Guid value, IFormFile image)
        {
            var path = Path.Combine(_env.WebRootPath, folder + "\\" + value);

            if (image.Length > 0)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(Path.Combine(path, image.FileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }
        }
        
        public void DeleteFolderForGivenId(string folder, Guid value)
        {
            var searchedPath = Path.Combine(_env.WebRootPath, folder + "\\" + value);

            if (Directory.Exists(searchedPath))
            {
                Directory.Delete(searchedPath, true);
            }
        }

        public void DeleteFileForGivenId(string folder, Guid value, string fileName)
        {
            var searchedPath = Path.Combine(_env.WebRootPath, folder + "\\" + value + "\\" + fileName);

            if (File.Exists(searchedPath))
            {
                File.Delete(searchedPath);
            }
        }

        public string GetPath(string folder, Guid value)
        {
            return Path.Combine(_env.WebRootPath, folder + "\\" + value);
        }

        public void CopyFile(string folder, Guid value)
        {
            var path = Path.Combine(_env.WebRootPath, "Default\\");

            foreach (var file in Directory.GetFiles(path))
            {
                if (Path.GetFileNameWithoutExtension(file) == folder)
                {
                    var targetPath = Path.Combine(_env.WebRootPath, folder + "\\" + value);

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
