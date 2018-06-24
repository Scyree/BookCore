using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class BookStateMiddleware : IBookStateMiddleware
    {
        private readonly IBookStateRepository _repository;

        public BookStateMiddleware(IBookStateRepository repository)
        {
            _repository = repository;
        }

        public List<BookState> GetAllStatesThatRatedThisBook(Guid bookId)
        {
            return _repository.GetAllBookStatesForBook(bookId).Where(state => state.Rate != 0).ToList();
        }

        public List<BookState> GetAllStatesChaptersForThisBook(Guid bookId)
        {
            return _repository.GetAllBookStatesForBook(bookId).Where(state => state.Chapters != null).ToList();
        }

        public BookState CheckIfBookAlreadyExists(Guid bookId, Guid userId)
        {
            return _repository.GetBookStateByBookAndUser(bookId, userId);
        }

        public List<BookState> GetAllBookStatesByUserId(Guid userId)
        {
            return _repository.GetAllBookStatesByUserId(userId);
        }

        public List<BookState> GetFavoriteBookStatesByUserId(Guid userId)
        {
            return _repository.GetFavoriteBookStatesByUserId(userId);
        }

        public void DeleteUserStates(Guid userId)
        {
            var userStates = _repository.GetAllBookStatesForUserId(userId);

            foreach (var state in userStates)
            {
                _repository.DeleteBookState(state);
            }
        }
    }
}
