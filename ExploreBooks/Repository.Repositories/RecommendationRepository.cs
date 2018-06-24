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

        public List<Recommendation> GetAllRecommendationsForBookId(Guid bookId)
        {
            return _databaseService.Recommendations.Where(recommendation => recommendation.BookId == bookId).ToList();
        }

        public List<Recommendation> GetAllRecommendationsMadeByUser(Guid userId)
        {
            return _databaseService.Recommendations.Where(recommendation => recommendation.UserId == userId).ToList();
        }

        public List<Recommendation> GetAllRecommendations()
        {
            return _databaseService.Recommendations.ToList();
        }

        public Recommendation GetRecommendation(Guid bookId, Guid recommendedBook, Guid userId)
        {
            return _databaseService.Recommendations.SingleOrDefault(recom => recom.BookRecommended == recommendedBook && recom.BookId == bookId && recom.UserId == userId);
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

        public Recommendation CheckIfAlreadyRecommended(Guid bookId, Guid recommendedBook, Guid userId)
        {
            return _databaseService.Recommendations.SingleOrDefault(recom => recom.UserId == userId && recom.BookId == bookId);
        }
    }
}
