using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IApplicationBookLogic
    {
        void ReadActions(Guid bookId, string userId, string actionName);
        IEnumerable<Book> GetBooksOfAUser(string userId);
        void AddToFavorites(Guid bookId, string userId);
        void RemoveFromFavorites(Guid bookId, string userId);
    }
}
