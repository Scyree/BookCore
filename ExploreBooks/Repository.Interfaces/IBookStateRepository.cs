using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IBookStateRepository
    {
        List<BookState> GetAllBookStatesByUserId(Guid userId);
        List<BookState> GetFavoriteBookStatesByUserId(Guid userId);
        List<BookState> GetAllBookStates();
        List<BookState> GetAllBookStatesForUserId(Guid userId);
        List<BookState> GetAllBookStatesForUserAndAction(Guid userId, int action);
        List<BookState> GetAllBookStatesForBook(Guid bookId);
        BookState GetBookStateByBookAndUser(Guid bookId, Guid userId);
        BookState GetBookStateById(Guid id);
        void CreateBookState(BookState bookState);
        void EditBookState(BookState bookState);
        void DeleteBookState(BookState bookState);
    }
}
