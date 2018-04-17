using System;
using System.Collections.Generic;
using Data.Domain.Entities;

namespace Data.Domain.Interfaces.Repositories
{
    public interface IAuthorBookRepository
    {
        List<AuthorBook> GetAllAuthorBooksBasedOnBookId(Guid bookId);
        List<AuthorBook> GetAllAuthorBooksBasedOnAuthorId(Guid authorId);
        AuthorBook GetAuthorBookById(Guid authorId, Guid bookId);
        void CreateAuthorBook(AuthorBook authorBook);
        void EditAuthorBook(AuthorBook authorBook);
        void DeleteAuthorBook(AuthorBook authorBook);
    }
}
