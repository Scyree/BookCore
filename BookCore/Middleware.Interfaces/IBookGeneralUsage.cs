using System;
using System.Collections.Generic;
using Data.Domain.Entities;

namespace Middleware.Interfaces
{
    public interface IBookGeneralUsage
    {
        Book GetBookInfoByDetails(string title, string description);
        IReadOnlyList<Book> GetAllBooks();
        Book GetBookById(Guid id);
        void CreateBook(Book book);
        void EditBook(Book book);
        void DeleteBook(Book book);
        Book CheckBook(string bookTitle);
        List<Book> GetBooks(string bookTitles);
    }
}
