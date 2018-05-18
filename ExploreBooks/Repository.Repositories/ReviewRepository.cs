using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public ReviewRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IReadOnlyList<Review>> GetAllReviews()
        {
            return await _databaseService.Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewById(Guid id)
        {
            return await _databaseService.Reviews.SingleOrDefaultAsync(review => review.Id == id);
        }

        public async Task CreateReview(Review review)
        {
            _databaseService.Reviews.Add(review);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditReview(Review review)
        {
            _databaseService.Reviews.Update(review);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteReview(Review review)
        {
            _databaseService.Reviews.Remove(review);

            await _databaseService.SaveChangesAsync();
        }
    }
}
