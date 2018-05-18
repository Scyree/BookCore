using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<IReadOnlyList<Book>> GetAllBooks();
        Task<Book> GetBookById(Guid id);
        Task<Book> GetBookInfoByDetails(string title, string description);
        Task CreateBook(Book book);
        Task EditBook(Book book);
        Task DeleteBook(Book book);
    }
}
