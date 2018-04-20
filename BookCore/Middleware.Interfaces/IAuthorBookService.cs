using System;
using System.Collections.Generic;
using Data.Domain.Entities;

namespace Middleware.Interfaces
{
    public interface IAuthorBookService
    {
        void CheckAuthorBook(Guid authorId, Guid bookId);
        List<AuthorBook> GetAllAuthorBooksBasedOnBookId(Guid bookId);
        List<AuthorBook> GetAllAuthorBooksBasedOnAuthorId(Guid authorId);
    }
}
