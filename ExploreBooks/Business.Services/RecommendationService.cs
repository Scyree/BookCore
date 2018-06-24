using System;
using System.Collections.Generic;
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

        public List<Recommendation> GetAllRecommendationsForBookId(Guid bookId)
        {
            return _repository.GetAllRecommendationsForBookId(bookId);
        }

        public List<Recommendation> GetAllRecommendationsMadeByUser(Guid userId)
        {
            return _repository.GetAllRecommendationsMadeByUser(userId);
        }

        public Recommendation GetRecommendation(Guid bookId, Guid recommendedBook, Guid userId)
        {
            return _repository.GetRecommendation(bookId, recommendedBook, userId);
        }

        public void MakeARecommendation(Guid bookId, Guid userId, Guid recommendedBook, string reason)
        {
            if (reason != null)
            {
                var checkIfAlreadyRecommended = _repository.CheckIfAlreadyRecommended(bookId, recommendedBook, userId);

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
