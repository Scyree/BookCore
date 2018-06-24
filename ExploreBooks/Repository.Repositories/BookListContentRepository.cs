using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class BookListContentRepository : IBookListContentRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public BookListContentRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public List<BookListContent> GetAllBookListContents()
        {
            return _databaseService.BookListContents.ToList();
        }

        public List<BookListContent> GetBookListsBasedOnBookId(Guid bookId)
        {
            return _databaseService.BookListContents.Where(bookList => bookList.BookListId == bookId).ToList();
        }

        public BookListContent GetBookListContentById(Guid id)
        {
            return _databaseService.BookListContents.SingleOrDefault(bookListContent => bookListContent.Id == id);
        }

        public void CreateBookListContent(BookListContent bookListContent)
        {
            _databaseService.BookListContents.Add(bookListContent);

            _databaseService.SaveChanges();
        }

        public void EditBookListContent(BookListContent bookListContent)
        {
            _databaseService.BookListContents.Update(bookListContent);

            _databaseService.SaveChanges();
        }

        public void DeleteBookListContent(BookListContent bookListContent)
        {
            _databaseService.BookListContents.Remove(bookListContent);

            _databaseService.SaveChanges();
        }
    }
}
