using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class BookStateGeneralUsage : IBookStateGeneralUsage
    {
        private readonly IBookStateRepository _repository;

        public BookStateGeneralUsage(IBookStateRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyList<BookState> GetAllBookStates()
        {
            return _repository.GetAllBookStates();
        }

        public BookState GetBookStateById(Guid id)
        {
            return _repository.GetBookStateById(id);
        }

        public void CreateBookState(BookState bookState)
        {
            _repository.CreateBookState(bookState);
        }

        public void EditBookState(BookState bookState)
        {
            _repository.EditBookState(bookState);
        }

        public void DeleteBookState(BookState bookState)
        {
            _repository.DeleteBookState(bookState);
        }

        public BookState CheckIfBookAlreadyExists(Guid bookId, Guid userId)
        {
            return _repository.GetAllBookStates().SingleOrDefault(state => state.TargetId == bookId && state.UserId == userId);
        }
    }
}
