using System;
using System.Collections.Generic;
using Data.Domain.Entities;

namespace Middleware.Interfaces
{
    public interface IReviewGeneralUsage
    {
        IReadOnlyList<Review> GetAllReviews();
        Review GetReviewById(Guid id);
        void CreateReview(Review review);
        void EditReview(Review review);
        void DeleteReview(Review review);
        IReadOnlyList<Review> GetReviewsByDate();
        IReadOnlyList<Review> GetReviewsBasedOnLikes();
    }
}
