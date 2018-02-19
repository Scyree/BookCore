using System;
using System.Collections.Generic;
using Data.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Data.Domain.Interfaces.Services
{
    public interface IReviewService
    {
        //From Repository
        IReadOnlyList<Review> GetAllReviews();
        Review GetReviewById(Guid id);
        void CreateReview(Review review);
        void EditReview(Review review);
        void DeleteReview(Review review);

        //New methods
        IReadOnlyList<Review> GetReviewsByDate();
        IReadOnlyList<Review> GetReviewsBasedOnLikes();
        IReadOnlyList<Review> GetOnlyFirstNumberOfReviews(int number);
        List<SelectListItem> GetRatingList();
        int GetNumberOfLikes(Guid reviewId);
        void Upvote(Guid reviewId, Guid userId);
        void Downvote(Guid reviewId, Guid userId);
    }
}
