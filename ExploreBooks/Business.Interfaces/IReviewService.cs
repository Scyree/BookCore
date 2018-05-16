using System;
using System.Collections.Generic;
using Domain.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Business.Interfaces
{
    public interface IReviewService
    {
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
