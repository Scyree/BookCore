using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IBookStateRepository _bookStateRepository;
        private readonly IBookStateMiddleware _stateService;
        private readonly IBookService _bookService;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly string _folder;

        public ApplicationUserServices(IApplicationUserRepository applicationRepository, IBookStateRepository bookStateRepository, IBookStateMiddleware stateService, IBookService bookService, IWorkingWithFiles fileManagement)
        {
            _applicationRepository = applicationRepository;
            _bookStateRepository = bookStateRepository;
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

        public IReadOnlyList<ApplicationUser> GetAllApplicationUsers()
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
                    _bookStateRepository.CreateBookState(searchedBook);
                }
                else
                {
                    searchedBook.State = value;
                    _bookStateRepository.EditBookState(searchedBook);
                }
            }
        }

        public IEnumerable<Book> GetBooksOfAUser(string userId)
        {
            var books = _bookStateRepository.GetAllBookStates().Where(state => state.UserId.ToString() == userId);
            var searchedBooks = new List<Book>();

            foreach (var book in books)
            {
                searchedBooks.Add(_bookService.GetBookById(book.TargetId));
            }

            return searchedBooks;
        }

        public void AddToFavorites(Guid bookId, string userId)
        {
            var book = _bookService.GetBookById(bookId);
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));

            if (book != null && user != null)
            {
                var searchedBook = _stateService.CheckIfBookAlreadyExists(bookId, Guid.Parse(userId));

                if (searchedBook == null)
                {
                    searchedBook = BookState.CreateBookState(
                        Guid.Parse(userId),
                        bookId
                    );

                    searchedBook.State = 2;
                    searchedBook.IsFavorite = true;
                    _bookStateRepository.CreateBookState(searchedBook);
                }
                else
                {
                    searchedBook.IsFavorite = true;
                    _bookStateRepository.EditBookState(searchedBook);
                }
            }
        }

        public void RemoveFromFavorites(Guid bookId, string userId)
        {
            var book = _bookService.GetBookById(bookId);
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));

            if (book != null && user != null)
            {
                var searchedBook = _stateService.CheckIfBookAlreadyExists(bookId, Guid.Parse(userId));

                if (searchedBook != null)
                {
                    searchedBook.IsFavorite = false;
                    _bookStateRepository.EditBookState(searchedBook);
                }
            }
        }
    }
}
