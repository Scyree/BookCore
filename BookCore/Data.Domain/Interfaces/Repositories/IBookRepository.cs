using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
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
