using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public BookRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public List<Book> GetAllBooksExcept(Guid bookId)
        {
            return _databaseService.Books.Where(book => book.Id != bookId).ToList();
        }

        public Book GetBookInfoByDetails(string title, string description)
        {
            return _databaseService.Books.FirstOrDefault(book => book.Title == title && book.Description == description);
        }

        public List<Book> GetAllBooks()
        {
            return _databaseService.Books.OrderBy(book => book.Title).ToList();
        }

        public List<Book> GetFirstNBooks(int skipNumber, int takeNumber)
        {
            return _databaseService.Books.OrderBy(book => book.Title).Skip(skipNumber).Take(takeNumber).ToList();
        }

        public List<Book> GetAllBooksContainingTheTitle(string title)
        {
            return _databaseService.Books.Where(book => book.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public Book GetBookBasedOnTitle(string title)
        {
            return _databaseService.Books.FirstOrDefault(book => book.Title.ToLower() == title.ToLower());
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
