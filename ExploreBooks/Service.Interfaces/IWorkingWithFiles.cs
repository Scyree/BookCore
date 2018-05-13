using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces
{
    public interface IWorkingWithFiles
    {
        Task CreateFile(string folder, IFormFile image);
        void DeleteFolder(string folder);
        void DeleteFileForGivenId(string folder, Guid value, string fileName);
        string GetPath(string folder, Guid value);
        void CopyFile(string folder, Guid value);
    }
}
