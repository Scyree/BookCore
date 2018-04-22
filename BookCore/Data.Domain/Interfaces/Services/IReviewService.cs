using System;
using System.Collections.Generic;
using Data.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Data.Domain.Interfaces.Services
{
    public interface IReviewService
    {
        //For LikeService
        int GetNumberOfLikes(Guid reviewId);
        void UpvoteReview(Guid reviewId, Guid userId);
        void DownvoteReview(Guid reviewId, Guid userId);

        //Review only methods
        IReadOnlyList<Review> GetOnlyFirstNumberOfReviews(int number);
        List<SelectListItem> GetRatingList();

        IReadOnlyList<Review> GetAllReviews(); 
        void CreateReview(Guid userId, Guid bookId, string description, double bookRating);
        void EditReview(Guid id, string description, double bookRating);
        void DeleteReview(Guid id);
        Review GetReviewById(Guid reviewId);
        
    }
}
