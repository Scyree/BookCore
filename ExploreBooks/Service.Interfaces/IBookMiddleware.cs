using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IBookMiddleware
    {
        Book GetBookInfoByDetails(string title, string description);
        Book CheckBook(string bookTitle);
        Book GetBookById(Guid bookId);
        List<Book> GetBooks(string bookTitles);
        List<Book> GetBooksForBookList(string bookTitles);
    }
}
