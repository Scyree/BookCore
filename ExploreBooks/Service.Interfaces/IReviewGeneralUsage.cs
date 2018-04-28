using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
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
