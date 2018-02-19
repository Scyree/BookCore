using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        IReadOnlyList<Review> GetAllReviews();
        Review GetReviewById(Guid id);
        void CreateReview(Review review);
        void EditReview(Review review);
        void DeleteReview(Review review);
        IList<Comment> GetAllComments(Guid reviewId);
    }
}
