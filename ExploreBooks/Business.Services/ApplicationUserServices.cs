using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Domain.Data;
using Microsoft.AspNetCore.Http;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class ApplicationUserServices : IApplicationUserServices
    {
        private readonly IApplicationUserRepository _applicationRepository;
        private readonly IBookStateGeneralUsage _stateService;
        private readonly IBookService _bookService;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly string _folder;

        public ApplicationUserServices(IApplicationUserRepository applicationRepository, IBookStateGeneralUsage stateService, IBookService bookService, IWorkingWithFiles fileManagement)
        {
            _applicationRepository = applicationRepository;
            _stateService = stateService;
            _bookService = bookService;
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

        public List<ApplicationUser> GetAllApplicationUsers()
        {
            return _applicationRepository.GetAllApplicationUsers();
        }

        public ApplicationUser GetApplicationUserById(Guid id)
        {
            return _applicationRepository.GetApplicationUserById(id);
        }

        public void CreateApplicationUser(ApplicationUser applicationUser)
        {
            _applicationRepository.CreateApplicationUser(applicationUser);
        }

        public void EditApplicationUser(ApplicationUser applicationUser)
        {
            _applicationRepository.EditApplicationUser(applicationUser);
        }

        public void DeleteApplicationUser(ApplicationUser applicationUser)
        {
            _applicationRepository.DeleteApplicationUser(applicationUser);
        }

        private int ConvertAction(string actionName)
        {
            if (actionName == "Plan to read")
            {
                return 1;
            }

            if (actionName == "Reading")
            {
                return 2;
            }

            if (actionName == "Read")
            {
                return 3;
            }

            return 0;
        }

        public void ReadActions(Guid bookId, string userId, string actionName)
        {
            var book = _bookService.GetBookById(bookId);
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var value = ConvertAction(actionName);

            if (book != null && user != null)
            {
                var searchedBook = _stateService.CheckIfBookAlreadyExists(bookId, Guid.Parse(userId));

                if (searchedBook == null)
                {
                    searchedBook = BookState.CreateBookState(
                        Guid.Parse(userId),
                        bookId
                    );

                    searchedBook.State = value;
                    _stateService.CreateBookState(searchedBook);
                }
                else
                {
                    searchedBook.State = value;
                    _stateService.EditBookState(searchedBook);
                }
            }
        }
    }
}
