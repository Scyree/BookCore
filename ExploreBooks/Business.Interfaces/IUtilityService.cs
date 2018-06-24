using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IUtilityService
    {
        List<BookState> GetAllBooksForUserId(string userId);
        List<BookState> GetFirstNBooksForUserId(string userId, int number);
        List<Post> GetAllPostsForUser(string userId);
        List<Post> GetFirstNPostsForUserId(string userId, int number);
        List<Book> GetMostPopularBooks();
        List<Notification> GetAllNotificationsForUser(string userId);
        Book GetBookById(Guid id);
        Author GetAuthorById(Guid id);
        Post GetPostById(Guid id);
        Comment GetCommentById(Guid commentId);
        BookState GetBookStateById(Guid bookId, Guid userId);
        ApplicationUser GetApplicationUser(string userId);
        string DisplayDate(DateTime date);
        string ConvertStateToAction(int state);
        Guid GetRandomBookId();
        Guid GetRecommendedBookId(Guid userId);
        void DeleteAllNotificationsForUser(string userId);
        void AddNews(string content);
        Guid GetBestRatedBooks();
    }
}
