using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Persistance;
using Data.Domain.Interfaces.Repositories;

namespace Business.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext _databaseService;

        public BookRepository(DatabaseContext databaseService)
        {
            _databaseService = databaseService;
        }

        public Book GetBookInfoByDetails(string title, string description)
        {
            return _databaseService.Books.SingleOrDefault(book => book.Title == title && book.Description == description);
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            return _databaseService.Books.ToList();
        }

        public Book GetBookById(Guid id)
        {
            return _databaseService.Books.SingleOrDefault(book => book.Id == id);
        }

        public void CreateBook(Book book)
        {
            _databaseService.Books.Add(book);

            _databaseService.SaveChanges();
        }

        public void EditBook(Book book)
        {
            _databaseService.Books.Update(book);

            _databaseService.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            _databaseService.Books.Remove(book);

            _databaseService.SaveChanges();
        }
    }
}
