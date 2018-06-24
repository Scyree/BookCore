using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class ApplicationBookLogic : IApplicationBookLogic
    {
        private readonly IApplicationUserRepository _applicationRepository;
        private readonly IBookStateRepository _bookStateRepository;
        private readonly IBookStateMiddleware _stateService;
        private readonly IBookRepository _bookService;

        public ApplicationBookLogic(IApplicationUserRepository applicationRepository, IBookStateRepository bookStateRepository, IBookStateMiddleware stateService, IBookRepository bookService)
        {
            _applicationRepository = applicationRepository;
            _bookStateRepository = bookStateRepository;
            _stateService = stateService;
            _bookService = bookService;
        }

        public bool CheckIfBookStateExists(Guid bookId, string userId)
        {
            var searchedBook = _stateService.CheckIfBookAlreadyExists(bookId, Guid.Parse(userId));

            if (searchedBook == null)
            {
                return false;
            }

            return true;
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

                    if (value == 3)
                    {
                        searchedBook.NumberOfPages = book.Pages;
                    }
                    if (value == 1)
                    {
                        searchedBook.NumberOfPages = 0;
                    }

                    _bookStateRepository.CreateBookState(searchedBook);
                }
                else
                {
                    searchedBook.State = value;
                    searchedBook.DateModified = DateTime.UtcNow;

                    if (value == 3)
                    {
                        searchedBook.NumberOfPages = book.Pages;
                    }
                    if (value == 1)
                    {
                        searchedBook.NumberOfPages = 0;
                    }

                    _bookStateRepository.EditBookState(searchedBook);
                }
            }
        }

        public List<Book> GetBooksOfAUser(string userId)
        {
            var books = _bookStateRepository.GetAllBookStatesByUserId(Guid.Parse(userId));
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

        public void ModifyBookPages(Guid bookId, string userId, string number)
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
                    searchedBook.NumberOfPages = Int32.Parse(number);

                    if (Int32.Parse(number) >= book.Pages)
                    {
                        searchedBook.State = 3;
                        searchedBook.NumberOfPages = book.Pages;
                    }

                    _bookStateRepository.CreateBookState(searchedBook);
                }
                else
                {
                    searchedBook.NumberOfPages = Int32.Parse(number);
                    searchedBook.State = 2;

                    if (Int32.Parse(number) >= book.Pages)
                    {
                        searchedBook.State = 3;
                        searchedBook.NumberOfPages = book.Pages;
                    }

                    _bookStateRepository.EditBookState(searchedBook);
                }
            }
        }

        public string GetNumberOfPages(Guid bookId, string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var book = _bookService.GetBookById(bookId);

            if (book != null && user != null)
            {
                var searchedBook = _stateService.CheckIfBookAlreadyExists(bookId, Guid.Parse(userId));

                if (searchedBook != null)
                {
                    return searchedBook.NumberOfPages.ToString();
                }
            }

            return null;
        }

        public int GetAllBooksNumber(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var count = 0;

            if (user != null)
            {
                count += _bookStateRepository.GetAllBookStatesByUserId(Guid.Parse(userId)).Count;
            }

            return count;
        }

        public int GetPlanToReadBooksNumber(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var count = 0;

            if (user != null)
            {
                count += _bookStateRepository.GetAllBookStatesForUserAndAction(Guid.Parse(userId), 1).Count;
            }

            return count;
        }

        public int GetReadingBooksNumber(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var count = 0;

            if (user != null)
            {
                count += _bookStateRepository.GetAllBookStatesForUserAndAction(Guid.Parse(userId), 2).Count;
            }

            return count;
        }

        public int GetReadBooksNumber(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var count = 0;

            if (user != null)
            {
                count += _bookStateRepository.GetAllBookStatesForUserAndAction(Guid.Parse(userId), 3).Count;
            }

            return count;
        }

        public List<Book> GetAllBooksBasedOnState(string userId, int givenState)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));

            if (user != null)
            {
                var bookStates = _bookStateRepository.GetAllBookStatesForUserAndAction(Guid.Parse(userId), givenState);
                var books = new List<Book>();

                foreach (var book in bookStates)
                {
                    books.Add(_bookService.GetBookById(book.TargetId));
                }

                return books;
            }

            return null;
        }

        public List<Book> GetFavoriteBooks(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));

            if (user != null)
            {
                var bookStates = _bookStateRepository.GetFavoriteBookStatesByUserId(Guid.Parse(userId));
                var books = new List<Book>();

                foreach (var book in bookStates)
                {
                    books.Add(_bookService.GetBookById(book.TargetId));
                }

                return books;
            }

            return null;
        }
    
        public bool CheckIfUserRatedThisBook(Guid userId, Guid bookId)
        {
            var state = _bookStateRepository.GetBookStateByBookAndUser(bookId, userId);

            if (state != null)
            {
                return state.Rate != 0;
            }

            return false;
        }

        public double GetUserRatingForBook(Guid userId, Guid bookId)
        {
            return _bookStateRepository.GetBookStateByBookAndUser(bookId, userId).Rate;
        }
        
        public double GetRatingsAverageForBook(Guid bookId)
        {
            var rates = _bookStateRepository.GetAllBookStatesForBook(bookId);
            var average = 5.0;

            if (rates.Count > 0)
            {
                var sum = 0.0;

                foreach (var rate in rates)
                {
                    sum += rate.Rate;
                }

                average = sum / rates.Count;
            }

            return Math.Round(average, 2);
        }

        public List<SelectListItem> GetRatingList()
        {
            var ratingList = new List<SelectListItem>
            {
                new SelectListItem { Text = "5 - Very good", Value = "5" },
                new SelectListItem { Text = "4 - Good", Value = "4" },
                new SelectListItem { Text = "3 - Normal", Value = "3" },
                new SelectListItem { Text = "2 - Not impressed", Value = "2" },
                new SelectListItem { Text = "1 - Hate it", Value = "1"}
            };

            return ratingList;
        }

        public string ConvertRateToText(double rate)
        {
            switch (rate)
            {
                case 1:
                    return "Hate it";
                case 2:
                    return "Not impressed";
                case 3:
                    return "Normal";
                case 4:
                    return "Good";
                default:
                    return "Very good";
            }
        }

        public void RateBook(Guid bookId, string userId, double value)
        {
            var bookState = _bookStateRepository.GetBookStateByBookAndUser(bookId, Guid.Parse(userId));

            if (bookState != null)
            {
                var editedBook = _bookService.GetBookById(bookId);
                bookState.Rate = Convert.ToInt32(value);
                _bookStateRepository.EditBookState(bookState);

                editedBook.FinalRate = GetRatingsAverageForBook(bookId);
                _bookService.EditBook(editedBook);
            }
            else
            {
                var book = BookState.CreateBookState(Guid.Parse(userId), bookId);
                var editedBook = _bookService.GetBookById(bookId);
                book.Rate = Convert.ToInt32(value);
                book.State = 2;
                _bookStateRepository.CreateBookState(book);

                editedBook.FinalRate = GetRatingsAverageForBook(bookId);
                _bookService.EditBook(editedBook);
            }
        }

        public bool CheckIfUserRecommendedChaptersForBook(Guid userId, Guid bookId)
        {
            var state = _bookStateRepository.GetBookStateByBookAndUser(bookId, userId);

            if (state != null)
            {
                return state.Chapters != null;
            }

            return false;
        }

        public string GetUserChaptersForBook(Guid userId, Guid bookId)
        {
            return _bookStateRepository.GetBookStateByBookAndUser(bookId, userId).Chapters;
        }
        
        public string GetChaptersAverageForBook(Guid bookId)
        {
            var chapters = _stateService.GetAllStatesChaptersForThisBook(bookId);
            var numberOfChapters = new int[24];
            var chaptersDictionary = new Dictionary<int, int>();

            for (var index = 0; index < 24; ++index)
            {
                numberOfChapters[index] = 0;
            }

            if (chapters.Count > 0)
            {
                foreach (var chapter in chapters)
                {
                    var dividedChapters = chapter.Chapters.Split(",");

                    foreach (var dividedChapter in dividedChapters)
                    {
                        ++numberOfChapters[Int32.Parse(dividedChapter)];
                    }
                }
            }

            var recommendedChapters = "";

            for (var index = 0; index < 24; ++index)
            {
                chaptersDictionary.Add(index, numberOfChapters[index]);
            }

            foreach (var chapter in chaptersDictionary.OrderByDescending(value => value.Value))
            {
                if (chapter.Value >= 1)
                {
                    recommendedChapters += chapter.Key + ", ";
                }
            }

            return recommendedChapters;
        }

        public void ChapterBook(Guid bookId, string userId, string chapters)
        {
            var bookState = _bookStateRepository.GetBookStateByBookAndUser(bookId, Guid.Parse(userId));

            if (bookState != null)
            {
                bookState.Chapters = chapters;
                _bookStateRepository.EditBookState(bookState);
            }
            else
            {
                var book = BookState.CreateBookState(Guid.Parse(userId), bookId);
                bookState.Chapters = chapters;
                book.State = 2;

                _bookStateRepository.CreateBookState(book);
            }
        }
    }
}
