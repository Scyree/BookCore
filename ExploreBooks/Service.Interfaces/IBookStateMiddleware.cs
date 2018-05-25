using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IBookStateMiddleware
    {
        BookState CheckIfBookAlreadyExists(Guid bookId, Guid userId);
        IReadOnlyList<BookState> GetAllBookStatesByUserId(Guid userId);
    }
}
