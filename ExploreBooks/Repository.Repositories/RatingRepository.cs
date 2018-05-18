using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public RatingRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IReadOnlyList<Rating>> GetAllRatings()
        {
            return await _databaseService.Ratings.ToListAsync();
        }

        public async Task<Rating> GetRatingById(Guid id)
        {
            return await _databaseService.Ratings.SingleOrDefaultAsync(rating => rating.Id == id);
        }

        public async Task CreateRating(Rating rating)
        {
            _databaseService.Ratings.Add(rating);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditRating(Rating rating)
        {
            _databaseService.Ratings.Update(rating);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteRating(Rating rating)
        {
            _databaseService.Ratings.Remove(rating);

            await _databaseService.SaveChangesAsync();
        }
    }
}
