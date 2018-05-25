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
        
        public BookState CheckIfBookAlreadyExists(Guid bookId, Guid userId)
        {
            return _repository.GetAllBookStates().SingleOrDefault(state => state.TargetId == bookId && state.UserId == userId);
        }

        public IReadOnlyList<BookState> GetAllBookStatesByUserId(Guid userId)
        {
            return _repository.GetAllBookStates().Where(state => state.UserId == userId).ToList();
        }
    }
}
