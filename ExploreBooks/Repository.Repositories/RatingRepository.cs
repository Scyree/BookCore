using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
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

        public IReadOnlyList<Rating> GetAllRatings()
        {
            return _databaseService.Ratings.ToList();
        }

        public Rating GetRatingById(Guid id)
        {
            return _databaseService.Ratings.SingleOrDefault(rating => rating.Id == id);
        }

        public void CreateRating(Rating rating)
        {
            _databaseService.Ratings.Add(rating);

            _databaseService.SaveChanges();
        }

        public void EditRating(Rating rating)
        {
            _databaseService.Ratings.Update(rating);

            _databaseService.SaveChanges();
        }

        public void DeleteRating(Rating rating)
        {
            _databaseService.Ratings.Remove(rating);

            _databaseService.SaveChanges();
        }
    }
}
