using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IRecommendationService
    {
        List<Recommendation> GetAllRecommendationsForBookId(Guid bookId);
        List<Recommendation> GetAllRecommendationsMadeByUser(Guid userId);
        Recommendation GetRecommendation(Guid bookId, Guid recommendedBook, Guid userId);
        void MakeARecommendation(Guid bookId, Guid userId, Guid recommendedBook, string reason);
    }
}
