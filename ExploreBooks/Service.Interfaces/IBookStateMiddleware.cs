using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IBookStateMiddleware
    {
        List<BookState> GetAllStatesThatRatedThisBook(Guid bookId);
        List<BookState> GetAllStatesChaptersForThisBook(Guid bookId);
        BookState CheckIfBookAlreadyExists(Guid bookId, Guid userId);
        List<BookState> GetAllBookStatesByUserId(Guid userId);
        List<BookState> GetFavoriteBookStatesByUserId(Guid userId);
        void DeleteUserStates(Guid userId);
    }
}
