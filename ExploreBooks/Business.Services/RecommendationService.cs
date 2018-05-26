using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;

namespace Business.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _repository;

        public RecommendationService(IRecommendationRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyList<Recommendation> GetAllRecommendationsForBookId(Guid bookId)
        {
            var recommendations = _repository.GetAllRecommendations().Where(recommendation => recommendation.BookId == bookId).ToList();

            return recommendations;
        }

        public IReadOnlyList<Recommendation> GetAllRecommendationsMadeByUser(Guid userId)
        {
            var recommendations = _repository.GetAllRecommendations().Where(recommendation => recommendation.UserId == userId).ToList();

            return recommendations;
        }

        public void MakeARecommendation(Guid bookId, Guid userId, Guid recommendedBook, string reason)
        {
            if (reason != null)
            {
                var checkIfAlreadyRecommended = _repository.GetAllRecommendations().SingleOrDefault(recom => recom.UserId == userId && recom.BookId == bookId);

                if (checkIfAlreadyRecommended == null)
                {
                    var recommendation = Recommendation.CreateRecommendation(
                        bookId,
                        userId,
                        recommendedBook,
                        reason
                    );

                    _repository.CreateRecommendation(recommendation);
                }
                else
                {
                    checkIfAlreadyRecommended.Reason = reason;
                    checkIfAlreadyRecommended.BookRecommended = recommendedBook;

                    _repository.EditRecommendation(checkIfAlreadyRecommended);
                }
            }
        }
    }
}
