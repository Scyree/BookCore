using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Domain.Persistence;
using Microsoft.AspNetCore.Http;
using Service.Interfaces;

namespace Business.Services
{
    public class ApplicationUserServices : IApplicationUserServices
    {
        private readonly ApplicationDbContext _databaseService;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly string _folder;

        public ApplicationUserServices(ApplicationDbContext databaseService, IWorkingWithFiles fileManagement)
        {
            _databaseService = databaseService;
            _fileManagement = fileManagement;
            _folder = "profile";
        }

        public string GetNameOfTheSpecifiedId(string userId)
        {
            var user = _databaseService.Users.FirstOrDefault(usr => usr.Id == userId.ToString());
            return user.FirstName + " " + user.LastName;
        }

        public void CreatePicture(Guid value)
        {
            _fileManagement.CopyFile(_folder, value);
        }

        public async Task UpdatePicture(string path, IFormFile image)
        {
            if (image != null)
            {
                await _fileManagement.CreateFile(path, image);
            }
        }

        public string GetFolderWithFile(string userId)
        {
            var user = _databaseService.Users.FirstOrDefault(usr => usr.Id == userId.ToString());
            var path = "images" + "\\" + user.Folder + "\\" + user.ImageName;

            return path;
        }

        public bool CheckIfHasProfilePicture(string userId)
        {
            var user = _databaseService.Users.FirstOrDefault(usr => usr.Id == userId.ToString());

            return user.ImageName == "profile.jpg";
        }
    }
}
