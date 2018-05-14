using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Business.Interfaces
{
    public interface IApplicationUserServices
    {
        string GetNameOfTheSpecifiedId(string userId);
        void CreatePicture(Guid value);
        Task UpdatePicture(string path, IFormFile image);
        string GetFolderWithFile(string userId);
        bool CheckIfHasProfilePicture(string userId);
    }
}
