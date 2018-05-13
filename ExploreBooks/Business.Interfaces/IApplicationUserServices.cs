using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Business.Interfaces
{
    public interface IApplicationUserServices
    {
        string GetNameOfTheSpecifiedId(string id);
        void CreatePicture(Guid value);
        Task UpdatePicture(string path, IFormFile image);
    }
}
