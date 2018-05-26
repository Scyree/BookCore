using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IRecommendationRepository
    {
        IReadOnlyList<Recommendation> GetAllRecommendations();
        Recommendation GetRecommendationById(Guid id);
        void CreateRecommendation(Recommendation recommendation);
        void EditRecommendation(Recommendation recommendation);
        void DeleteRecommendation(Recommendation recommendation);
    }
}
