using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public RecommendationRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Recommendation> GetAllRecommendations()
        {
            return _databaseService.Recommendations.ToList();
        }

        public Recommendation GetRecommendationById(Guid id)
        {
            return _databaseService.Recommendations.SingleOrDefault(recommendation => recommendation.Id == id);
        }

        public void CreateRecommendation(Recommendation recommendation)
        {
            _databaseService.Recommendations.Add(recommendation);

            _databaseService.SaveChanges();
        }

        public void EditRecommendation(Recommendation recommendation)
        {
            _databaseService.Recommendations.Update(recommendation);

            _databaseService.SaveChanges();
        }

        public void DeleteRecommendation(Recommendation recommendation)
        {
            _databaseService.Recommendations.Remove(recommendation);

            _databaseService.SaveChanges();
        }
    }
}
