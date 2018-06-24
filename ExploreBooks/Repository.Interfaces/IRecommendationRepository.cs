using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IRecommendationRepository
    {
        List<Recommendation> GetAllRecommendationsForBookId(Guid bookId);
        List<Recommendation> GetAllRecommendationsMadeByUser(Guid userId);
        List<Recommendation> GetAllRecommendations();
        Recommendation GetRecommendation(Guid bookId, Guid recommendedBook, Guid userId);
        Recommendation GetRecommendationById(Guid id);
        Recommendation CheckIfAlreadyRecommended(Guid bookId, Guid recommendedBook, Guid userId);
        void CreateRecommendation(Recommendation recommendation);
        void EditRecommendation(Recommendation recommendation);
        void DeleteRecommendation(Recommendation recommendation);
    }
}
