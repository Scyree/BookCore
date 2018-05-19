using System;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IBookStateMiddleware
    {
        BookState CheckIfBookAlreadyExists(Guid bookId, Guid userId);
    }
}
