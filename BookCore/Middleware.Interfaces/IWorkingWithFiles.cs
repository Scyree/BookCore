using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Middleware.Interfaces
{
    public interface IWorkingWithFiles
    {
        Task CreateFile(string folder, Guid value, IFormFile image);
        void DeleteFolder(string folder);
        void DeleteFileForGivenId(string folder, Guid value, string fileName);
        string GetPath(string folder, Guid value);
        void CopyFile(string folder, Guid value);
    }
}
