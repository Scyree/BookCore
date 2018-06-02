using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class ApplicationBookLogic : IApplicationBookLogic
    {
        private readonly IApplicationUserRepository _applicationRepository;
        private readonly IBookStateRepository _bookStateRepository;
        private readonly IBookStateMiddleware _stateService;
        private readonly IBookService _bookService;

        public ApplicationBookLogic(IApplicationUserRepository applicationRepository, IBookStateRepository bookStateRepository, IBookStateMiddleware stateService, IBookService bookService)
        {
            _applicationRepository = applicationRepository;
            _bookStateRepository = bookStateRepository;
            _stateService = stateService;
            _bookService = bookService;
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

        public int GetAllBooksNumber(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var count = 0;

            if (user != null)
            {
                count += _bookStateRepository.GetAllBookStates().Count(state => state.UserId == Guid.Parse(user.Id));

                return count;
            }

            return count;
        }

        public int GetPlanToReadBooksNumber(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var count = 0;

            if (user != null)
            {
                count += _bookStateRepository.GetAllBookStates().Count(state => state.UserId == Guid.Parse(user.Id) && state.State == 1);

                return count;
            }

            return count;
        }

        public int GetReadingBooksNumber(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var count = 0;

            if (user != null)
            {
                count += _bookStateRepository.GetAllBookStates().Count(state => state.UserId == Guid.Parse(user.Id) && state.State == 2);

                return count;
            }

            return count;
        }

        public int GetReadBooksNumber(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var count = 0;

            if (user != null)
            {
                count += _bookStateRepository.GetAllBookStates().Count(state => state.UserId == Guid.Parse(user.Id) && state.State == 3);

                return count;
            }

            return count;
        }

        public IReadOnlyList<Book> GetAllBooksBasedOnState(string userId, int givenState)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));

            if (user != null)
            {
                var bookStates = _bookStateRepository.GetAllBookStates().Where(state => state.UserId == Guid.Parse(userId) && state.State == givenState);
                var books = new List<Book>();

                foreach (var book in bookStates)
                {
                    books.Add(_bookService.GetBookById(book.TargetId));
                }

                return books;
            }

            return null;
        }
    }
}
