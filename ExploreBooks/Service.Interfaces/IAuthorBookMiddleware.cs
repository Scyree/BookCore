using Domain.Data;
using System;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IAuthorBookMiddleware
    {
        List<AuthorBook> GetAllAuthorBooksBasedOnBookId(Guid bookId);
        List<AuthorBook> GetAllAuthorBooksBasedOnAuthorId(Guid authorId);
        void CheckAuthorBook(Guid authorId, Guid bookId);
        void DeleteForAuthorId(Guid authorId);
        void DeleteForBookId(Guid bookId);
    }
}
