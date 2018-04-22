using System;
using System.Linq;
using System.Collections.Generic;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Middleware.Interfaces;

namespace Business.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewGeneralUsage _reviewService;
        private readonly ILikeService _likeService;
        private readonly ICommentService _commentService;

        public ReviewService(IReviewGeneralUsage reviewService, ILikeService likeService, ICommentService commentService)
        {
            _reviewService = reviewService;
            _likeService = likeService;
            _commentService = commentService;
        }

        public int GetNumberOfLikes(Guid reviewId)
        {
            return _likeService.GetNumberOfLikes(reviewId);
        }

        public void UpvoteReview(Guid reviewId, Guid userId)
        {
            _likeService.Upvote(reviewId, userId);
        }

        public void DownvoteReview(Guid reviewId, Guid userId)
        {
            _likeService.Downvote(reviewId, userId);
        }
        
        public IReadOnlyList<Review> GetOnlyFirstNumberOfReviews(int number)
        {
            return _reviewService.GetReviewsBasedOnLikes().Take(number).ToList();
        }

        public List<SelectListItem> GetRatingList()
        {
            var ratingList = new List<SelectListItem>
            {
                new SelectListItem { Text = "5 - Very good", Value = "5" },
                new SelectListItem { Text = "4 - Good", Value = "4" },
                new SelectListItem { Text = "3 - Normal", Value = "3" },
                new SelectListItem { Text = "2 - Not impressed", Value = "2" },
                new SelectListItem { Text = "1 - Hate it", Value = "1"}
            };

            return ratingList;
        }

        public IReadOnlyList<Review> GetAllReviews()
        {
            var reviews = _reviewService.GetReviewsByDate();

            foreach (var review in reviews)
            {
                review.Comments = _commentService.GetAllComments(review.Id).ToList();
            }

            return reviews;
        }

        public void CreateReview(Guid userId, Guid bookId, string description, double bookRating)
        {
            var review = Review.CreateReview(
                bookRating,
                description,
                userId,
                bookId
            );

            _reviewService.CreateReview(review);
        }

        public void EditReview(Guid id, string description, double bookRating)
        {
            var reviewToBeEdited = _reviewService.GetReviewById(id);

            if (reviewToBeEdited != null)
            {
                if (description != null)
                {
                    reviewToBeEdited.Description = description;
                }

                reviewToBeEdited.BookRating = bookRating;

                _reviewService.EditReview(reviewToBeEdited);
            }
        }

        public void DeleteReview(Guid id)
        {
            var reviewToBeDeleted = _reviewService.GetReviewById(id);

            if (reviewToBeDeleted != null)
            {
                _reviewService.DeleteReview(reviewToBeDeleted);
            }
        }

        public Review GetReviewById(Guid reviewId)
        {
            return _reviewService.GetReviewById(reviewId);
        }

        private void DeleteNegativeReviews(Guid reviewId)
        {
            if (GetNumberOfLikes(reviewId) <= -20)
            {
                var likeList = _likeService.GetAllLikes().Where(likes => likes.TargetId == reviewId).ToList();

                foreach (var like in likeList)
                {
                    _likeService.DeleteLike(like);
                }

                _reviewService.DeleteReview(_reviewService.GetReviewById(reviewId));
            }
        }
    }
}
