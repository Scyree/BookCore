using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class BookStateRepository : IBookStateRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public BookStateRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<BookState> GetAllBookStates()
        {
            return _databaseService.BookStates.ToList();
        }

        public BookState GetBookStateById(Guid id)
        {
            return _databaseService.BookStates.SingleOrDefault(bookState => bookState.Id == id);
        }

        public void CreateBookState(BookState bookState)
        {
            _databaseService.BookStates.Add(bookState);

            _databaseService.SaveChanges();
        }

        public void EditBookState(BookState bookState)
        {
            _databaseService.BookStates.Update(bookState);

            _databaseService.SaveChanges();
        }

        public void DeleteBookState(BookState bookState)
        {
            _databaseService.BookStates.Remove(bookState);

            _databaseService.SaveChanges();
        }
    }
}
