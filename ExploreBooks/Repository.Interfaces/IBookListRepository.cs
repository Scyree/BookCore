using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IBookListRepository
    {
        List<BookList> GetAllBookLists();
        BookList GetBookListById(Guid id);
        void CreateBookList(BookList bookList);
        void EditBookList(BookList bookList);
        void DeleteBookList(BookList bookList);
    }
}
