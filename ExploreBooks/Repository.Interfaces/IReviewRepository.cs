using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IReviewRepository
    {
        Task<IReadOnlyList<Review>> GetAllReviews();
        Task<Review> GetReviewById(Guid id);
        Task CreateReview(Review review);
        Task EditReview(Review review);
        Task DeleteReview(Review review);
    }
}
