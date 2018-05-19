using Domain.Data;
using System;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IAuthorBookRepository
    {
        IReadOnlyList<AuthorBook> GetAllAuthorBooksBasedOnBookId(Guid bookId);
        IReadOnlyList<AuthorBook> GetAllAuthorBooksBasedOnAuthorId(Guid authorId);
        AuthorBook GetAuthorBookById(Guid authorId, Guid bookId);
        void CreateAuthorBook(AuthorBook authorBook);
        void EditAuthorBook(AuthorBook authorBook);
        void DeleteAuthorBook(AuthorBook authorBook);
    }
}
