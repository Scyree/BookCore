using System;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class ApplicationPictureLogic : IApplicationPictureLogic
    {
        private readonly IApplicationUserRepository _applicationRepository;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly string _folder;

        public ApplicationPictureLogic(IApplicationUserRepository applicationRepository, IWorkingWithFiles fileManagement)
        {
            _applicationRepository = applicationRepository;
            _fileManagement = fileManagement;
            _folder = "profile";
        }

        public string GetNameOfTheSpecifiedId(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
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
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var path = "images" + "\\" + user.Folder + "\\" + user.ImageName;

            return path;
        }

        public bool CheckIfHasProfilePicture(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));

            return user.ImageName == "profile.jpg";
        }

    }
}
