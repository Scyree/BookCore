using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAllBooksExcept(Guid bookId);
        List<Book> GetAllBooks();
        List<Book> GetFirstNBooks(int skipNumber, int takeNumber);
        Book GetBookInfoByDetails(string title, string description);
        List<Book> GetAllBooksContainingTheTitle(string title);
        Book GetBookBasedOnTitle(string title);
        Book GetBookById(Guid id);
        void CreateBook(Book book);
        void EditBook(Book book);
        void DeleteBook(Book book);
    }
}
