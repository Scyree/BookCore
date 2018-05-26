using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IRecommendationService
    {
        IReadOnlyList<Recommendation> GetAllRecommendationsForBookId(Guid bookId);
        IReadOnlyList<Recommendation> GetAllRecommendationsMadeByUser(Guid userId);
        void MakeARecommendation(Guid bookId, Guid userId, Guid recommendedBook, string reason);
    }
}
