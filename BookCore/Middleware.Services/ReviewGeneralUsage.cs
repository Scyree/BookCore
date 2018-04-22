using System;
using System.Linq;
using System.Collections.Generic;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Middleware.Interfaces;

namespace Middleware.Services
{
    public class ReviewGeneralUsage : IReviewGeneralUsage
    {
        private readonly IReviewRepository _repository;

        public ReviewGeneralUsage(IReviewRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyList<Review> GetAllReviews()
        {
            return _repository.GetAllReviews();
        }

        public Review GetReviewById(Guid id)
        {
            return _repository.GetReviewById(id);
        }

        public void CreateReview(Review review)
        {
            _repository.CreateReview(review);
        }

        public void EditReview(Review review)
        {
            _repository.EditReview(review);
        }

        public void DeleteReview(Review review)
        {
            _repository.DeleteReview(review);
        }

        public IReadOnlyList<Review> GetReviewsByDate()
        {
            return _repository.GetAllReviews().OrderByDescending(review => review.Date).ToList();
        }

        public IReadOnlyList<Review> GetReviewsBasedOnLikes()
        {
            return _repository.GetAllReviews().OrderByDescending(review => review.Likes).ToList();
        }
    }
}
