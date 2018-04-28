using Domain.Data;
using System;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IAuthorBookService
    {
        void CheckAuthorBook(Guid authorId, Guid bookId);
        List<AuthorBook> GetAllAuthorBooksBasedOnBookId(Guid bookId);
        List<AuthorBook> GetAllAuthorBooksBasedOnAuthorId(Guid authorId);
        void DeleteForAuthorId(Guid authorId);
        void DeleteForBookId(Guid bookId);
    }
}
