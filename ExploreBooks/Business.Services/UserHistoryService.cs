using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Service.Interfaces;

namespace Business.Services
{
    public class UserHistoryService : IUserHistoryService
    {
        private readonly IReviewService _reviewService;
        private readonly IBookStateGeneralUsage _stateService;
        private readonly IBookGeneralUsage _bookService;

        public UserHistoryService(IReviewService reviewService, IBookStateGeneralUsage stateService, IBookGeneralUsage bookService)
        {
            _reviewService = reviewService;
            _stateService = stateService;
            _bookService = bookService;
        }

        public ICollection<BookState> GetAllBooksForUserId(string userId)
        {
            var books = _stateService.GetAllBookStates().Where(state => state.UserId.ToString() == userId);
            var bookActivity = new List<BookState>();

            foreach (var book in books)
            {
                bookActivity.Add(book);
            }

            return bookActivity;
        }

        public IEnumerable<BookState> GetFirstNBooksForUserId(string userId, int number)
        {
            var books = _stateService.GetAllBookStates().Where(state => state.UserId.ToString() == userId);
            var bookActivity = new List<BookState>();

            foreach (var book in books)
            {
                bookActivity.Add(book);
            }

            if (bookActivity.Count < number)
            {
                return bookActivity;
            }

            return bookActivity.Take(number);
        }

        public ICollection<Review> GetAllReviewsForUser(string userId)
        {
            var reviews = _reviewService.GetAllReviews().Where(review => review.UserId.ToString() == userId);
            var reviewActivity = new List<Review>();

            foreach (var review in reviews)
            {
                reviewActivity.Add(review);
            }

            return reviewActivity;
        }

        public IEnumerable<Review> GetFirstNReviewsForUserId(string userId, int number)
        {
            var reviews = _reviewService.GetAllReviews().Where(review => review.UserId.ToString() == userId);
            var reviewActivity = new List<Review>();

            foreach (var review in reviews)
            {
                reviewActivity.Add(review);
            }

            if (reviewActivity.Count < number)
            {
                return reviewActivity;
            }

            return reviewActivity.Take(number);
        }

        public Book ConvertFromBookStateToBook(Guid bookId)
        {
            var book = _bookService.GetBookById(bookId);
            
            return book;
        }
    }
}
