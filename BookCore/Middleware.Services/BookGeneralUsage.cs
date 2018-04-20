using System;
using System.Linq;
using System.Collections.Generic;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Middleware.Interfaces;

namespace Middleware.Services
{
    public class BookGeneralUsage : IBookGeneralUsage
    {
        private readonly IBookRepository _repository;
        private readonly IWorkingWithFiles _fileManagement;
        private readonly string _folder;

        public BookGeneralUsage(IBookRepository repository, IWorkingWithFiles fileManagement)
        {
            _repository = repository;
            _fileManagement = fileManagement;
            _folder = "Books";
        }

        public Book GetBookInfoByDetails(string title, string description)
        {
            return _repository.GetBookInfoByDetails(title, description);
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            return _repository.GetAllBooks();
        }

        public Book GetBookById(Guid id)
        {
            return _repository.GetBookById(id);
        }

        public void CreateBook(Book book)
        {
            _repository.CreateBook(book);
        }

        public void EditBook(Book book)
        {
            _repository.EditBook(book);
        }

        public void DeleteBook(Book book)
        {
            _repository.DeleteBook(book);
            _fileManagement.DeleteFolder(book.Folder);
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
                    "Momentan nu exista nicio descriere a cartii",
                    path,
                    imageName,
                    "Momentan nu exista niciun detaliu legat de carte"
                );

                _repository.CreateBook(book);

                return book;
            }

            return check;
        }

        public List<Book> GetBooks(string bookTitles)
        {
            var books = bookTitles.Split(",");
            var bookList = new List<Book>();

            foreach (var book in books)
            {
                bookList.Add(CheckBook(book));
            }

            return bookList;
        }
    }
}
