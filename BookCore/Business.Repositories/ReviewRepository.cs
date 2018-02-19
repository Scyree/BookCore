using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Persistence;

namespace Business.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public ReviewRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Review> GetAllReviews()
        {
            return _databaseService.Reviews.ToList();
        }

        public Review GetReviewById(Guid id)
        {
            return _databaseService.Reviews.SingleOrDefault(review => review.Id == id);
        }

        public void CreateReview(Review review)
        {
            _databaseService.Reviews.Add(review);

            _databaseService.SaveChanges();
        }

        public void EditReview(Review review)
        {
            _databaseService.Reviews.Update(review);

            _databaseService.SaveChanges();
        }

        public void DeleteReview(Review review)
        {
            _databaseService.Reviews.Remove(review);

            _databaseService.SaveChanges();
        }

        public IList<Comment> GetAllComments(Guid reviewId)
        {
            return _databaseService.Comments.Where(comments => comments.TargetId == reviewId).ToList();
        }
    }
}
