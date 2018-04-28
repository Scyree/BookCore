using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IBookRepository
    {
        IReadOnlyList<Book> GetAllBooks();
        Book GetBookById(Guid id);
        Book GetBookInfoByDetails(string title, string description);
        void CreateBook(Book book);
        void EditBook(Book book);
        void DeleteBook(Book book);
    }
}
