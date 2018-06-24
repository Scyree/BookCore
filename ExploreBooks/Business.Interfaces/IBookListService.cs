using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IBookListService
    {
        List<BookList> GetAllBookLists();
        void CreateBookList(Guid userId, string title, string description, string books);
        BookList GetBookListById(Guid id);
    }
}
