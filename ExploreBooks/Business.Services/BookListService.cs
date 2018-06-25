using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class BookListService : IBookListService
    {
        private readonly IBookListRepository _bookListRepository;
        private readonly IBookListContentRepository _bookRepository;
        private readonly IBookMiddleware _bookService;
        private readonly IPostService _postService;

        public BookListService(IBookListRepository bookListRepository, IBookListContentRepository bookRepository, IBookMiddleware bookService, IPostService postService)
        {
            _bookListRepository = bookListRepository;
            _bookRepository = bookRepository;
            _bookService = bookService;
            _postService = postService;
        }

        public List<BookList> GetAllBookLists()
        {
            var books = _bookListRepository.GetAllBookLists();

            foreach (var book in books)
            {
                book.Books = _bookRepository.GetBookListsBasedOnBookId(book.Id).ToList();
                book.Posts = _postService.GetAllPosts().Where(post => post.TargetId == book.Id).ToList();
            }

            return books;
        }

        public void CreateBookList(Guid userId, string title, string description, List<string> books)
        {
            var bookList = new List<Book>();

            if (userId.ToString() != null)
            {
                foreach (var book in books)
                {
                    if (book != null)
                    {
                        var correctBook = _bookService.GetBookById(Guid.Parse(book));

                        if (correctBook != null)
                        {
                            bookList.Add(correctBook);
                        }
                    }
                }

                var bookForMood = BookList.CreateBookList(
                    userId,
                    title,
                    description
                );

                _bookListRepository.CreateBookList(bookForMood);

                foreach (var book in bookList)
                {
                    var bookWithinList = BookListContent.Create(
                        bookForMood.Id,
                        book.Id
                    );

                    _bookRepository.CreateBookListContent(bookWithinList);
                }
            }
        }

        public BookList GetBookListById(Guid id)
        {
            var book = _bookListRepository.GetBookListById(id);

            if (book != null)
            {
                book.Books = _bookRepository.GetBookListsBasedOnBookId(book.Id).ToList();
                book.Posts = _postService.GetAllPosts().Where(post => post.TargetId == book.Id).ToList();

                return book;
            }

            return null;
        }
    }
}
