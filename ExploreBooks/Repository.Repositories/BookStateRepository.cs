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

        public List<BookState> GetAllBookStatesByUserId(Guid userId)
        {
            return _databaseService.BookStates.Where(state => state.UserId == userId).ToList();
        }

        public List<BookState> GetFavoriteBookStatesByUserId(Guid userId)
        {
            return _databaseService.BookStates.Where(state => state.UserId == userId && state.IsFavorite).ToList();
        }

        public List<BookState> GetAllBookStates()
        {
            return _databaseService.BookStates.ToList();
        }

        public List<BookState> GetAllBookStatesForUserId(Guid userId)
        {
            return _databaseService.BookStates.Where(user => user.UserId == userId).ToList();
        }

        public List<BookState> GetAllBookStatesForUserAndAction(Guid userId, int action)
        {
            return _databaseService.BookStates.Where(state => state.UserId == userId && state.State == action).ToList();
        }

        public List<BookState> GetAllBookStatesForBook(Guid bookId)
        {
            return _databaseService.BookStates.Where(state => state.TargetId == bookId).ToList();
        }

        public BookState GetBookStateByBookAndUser(Guid bookId, Guid userId)
        {
            return _databaseService.BookStates.SingleOrDefault(state => state.TargetId == bookId && state.UserId == userId);
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
