using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IBookStateRepository
    {
        IReadOnlyList<BookState> GetAllBookStates();
        BookState GetBookStateById(Guid id);
        void CreateBookState(BookState bookState);
        void EditBookState(BookState bookState);
        void DeleteBookState(BookState bookState);
    }
}
