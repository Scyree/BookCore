using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class BookListRepository : IBookListRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public BookListRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public List<BookList> GetAllBookLists()
        {
            return _databaseService.BookLists.ToList();
        }

        public BookList GetBookListById(Guid id)
        {
            return _databaseService.BookLists.SingleOrDefault(bookList => bookList.Id == id);
        }

        public void CreateBookList(BookList bookList)
        {
            _databaseService.BookLists.Add(bookList);

            _databaseService.SaveChanges();
        }

        public void EditBookList(BookList bookList)
        {
            _databaseService.BookLists.Update(bookList);

            _databaseService.SaveChanges();
        }

        public void DeleteBookList(BookList bookList)
        {
            _databaseService.BookLists.Remove(bookList);

            _databaseService.SaveChanges();
        }
    }
}
