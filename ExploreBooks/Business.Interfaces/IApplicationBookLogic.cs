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
        int GetAllBooksNumber(string userId);
        int GetPlanToReadBooksNumber(string userId);
        int GetReadingBooksNumber(string userId);
        int GetReadBooksNumber(string userId);
        IReadOnlyList<Book> GetAllBooksBasedOnState(string userId, int givenState);
        IReadOnlyList<Book> GetFavoriteBooks(string userId);
    }
}
