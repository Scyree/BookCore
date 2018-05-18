using Domain.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAuthorBookRepository
    {
        Task<IReadOnlyList<AuthorBook>> GetAllAuthorBooksBasedOnBookId(Guid bookId);
        Task<IReadOnlyList<AuthorBook>> GetAllAuthorBooksBasedOnAuthorId(Guid authorId);
        Task<AuthorBook> GetAuthorBookById(Guid authorId, Guid bookId);
        Task CreateAuthorBook(AuthorBook authorBook);
        Task EditAuthorBook(AuthorBook authorBook);
        Task DeleteAuthorBook(AuthorBook authorBook);
    }
}
