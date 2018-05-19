using Domain.Data;
using System;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IAuthorBookMiddleware
    {
        IReadOnlyList<AuthorBook> GetAllAuthorBooksBasedOnBookId(Guid bookId);
        IReadOnlyList<AuthorBook> GetAllAuthorBooksBasedOnAuthorId(Guid authorId);
        void CheckAuthorBook(Guid authorId, Guid bookId);
        void DeleteForAuthorId(Guid authorId);
        void DeleteForBookId(Guid bookId);
    }
}
