using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IReviewRepository
    {
        IReadOnlyList<Review> GetAllReviews();
        Review GetReviewById(Guid id);
        void CreateReview(Review review);
        void EditReview(Review review);
        void DeleteReview(Review review);
    }
}
