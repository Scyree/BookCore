using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IUserHistoryService
    {
        ICollection<BookState> GetAllBooksForUserId(string userId);
        IEnumerable<BookState> GetFirstNBooksForUserId(string userId, int number);
        ICollection<Review> GetAllReviewsForUser(string userId);
        IEnumerable<Review> GetFirstNReviewsForUserId(string userId, int number);
        Book ConvertFromBookStateToBook(Guid id);
    }
}
