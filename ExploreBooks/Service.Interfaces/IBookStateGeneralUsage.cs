using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IBookStateGeneralUsage
    {
        IReadOnlyList<BookState> GetAllBookStates();
        BookState GetBookStateById(Guid id);
        void CreateBookState(BookState bookState);
        void EditBookState(BookState bookState);
        void DeleteBookState(BookState bookState);
        BookState CheckIfBookAlreadyExists(Guid bookId, Guid userId);
    }
}
