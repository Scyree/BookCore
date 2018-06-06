using System;
using System.Linq;
using System.Collections.Generic;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class BookMiddleware : IBookMiddleware
    {
        private readonly IBookRepository _repository;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly string _folder;

        public BookMiddleware(IBookRepository repository, IWorkingWithFiles fileManagement)
        {
            _repository = repository;
            _fileManagement = fileManagement;
            _folder = "books";
        }

        public Book GetBookInfoByDetails(string title, string description)
        {
            return _repository.GetBookInfoByDetails(title, description);
        }
        
        public Book CheckBook(string bookTitle)
        {
            var check = _repository.GetAllBooks().SingleOrDefault(book => book.Title == bookTitle);

            if (check == null)
            {
                var value = Guid.NewGuid();
                var path = _folder + "\\" + value;
                var imageName = _folder + ".jpg";

                _fileManagement.CopyFile(_folder, value);

                var book = Book.CreateBook(
                    bookTitle,
                    "No description for this book at the moment..",
                    path,
                    imageName,
                    "There are mysteries surrounding this book.."
                );

                _repository.CreateBook(book);

                return book;
            }

            return check;
        }

        public IReadOnlyList<Book> GetBooks(string bookTitles)
        {
            var books = bookTitles.Split(",");
            var bookList = new List<Book>();

            foreach (var book in books)
            {
                bookList.Add(CheckBook(book));
            }

            return bookList;
        }

        public Book GetBookById(Guid bookId)
        {
            return _repository.GetBookById(bookId);
        }
    }
}
