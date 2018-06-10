using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class BooksForMoodService : IBooksForMoodService
    {
        private readonly IBooksForMoodRepository _booksForMoodRepository;
        private readonly IBooksWithinBooksForMoodRepository _bookRepository;
        private readonly IBookMiddleware _bookService;
        private readonly IPostService _postService;

        public BooksForMoodService(IBooksForMoodRepository booksForMoodRepository, IBooksWithinBooksForMoodRepository bookRepository, IBookMiddleware bookService, IPostService postService)
        {
            _booksForMoodRepository = booksForMoodRepository;
            _bookRepository = bookRepository;
            _bookService = bookService;
            _postService = postService;
        }

        public IReadOnlyList<BooksForMood> GetAllBooksForMoods()
        {
            var books = _booksForMoodRepository.GetAllBooksForMoods();

            foreach (var book in books)
            {
                book.Books = _bookRepository.GetAllBooksWithinBooksForMood().Where(mood => mood.BooksForMoodId == book.Id).ToList();
                book.Posts = _postService.GetAllPosts().Where(post => post.TargetId == book.Id).ToList();
            }

            return books;
        }

        public void CreateBooksForMood(Guid userId, string title, string description, string books)
        {
            var bookList = _bookService.GetBooksForBooksForMood(books);

            var bookForMood = BooksForMood.CreateBooksForMood(
                userId,
                title,
                description
            );

            _booksForMoodRepository.CreateBooksForMood(bookForMood);

            foreach (var book in bookList)
            {
                var bookWithinList = BooksWithinBooksForMood.Create(
                    bookForMood.Id,
                    book.Id
                );

                _bookRepository.CreateBooksWithinBooksForMood(bookWithinList);
            }
        }

        public BooksForMood GetBooksForMoodById(Guid id)
        {
            var book = _booksForMoodRepository.GetBooksForMoodById(id);

            if (book != null)
            {
                book.Books = _bookRepository.GetAllBooksWithinBooksForMood().Where(mood => mood.BooksForMoodId == book.Id).ToList();
                book.Posts = _postService.GetAllPosts().Where(post => post.TargetId == book.Id).ToList();

                return book;
            }

            return null;
        }
    }
}
