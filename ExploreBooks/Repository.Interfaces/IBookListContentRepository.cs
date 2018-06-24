using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IBookListContentRepository
    {
        List<BookListContent> GetAllBookListContents();
        List<BookListContent> GetBookListsBasedOnBookId(Guid bookId);
        BookListContent GetBookListContentById(Guid id);
        void CreateBookListContent(BookListContent bookListContent);
        void EditBookListContent(BookListContent bookListContent);
        void DeleteBookListContent(BookListContent bookListContent);
    }
}
