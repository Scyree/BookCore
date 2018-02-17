﻿using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Business.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;

        public ReviewService(IReviewRepository repository)
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
            _repository.EditReview(review);
        }

        public IReadOnlyList<Review> GetReviewsByDate()
        {
            return _repository.GetAllReviews().OrderByDescending(review => review.Date).ToList();
        }

        public IReadOnlyList<Review> GetReviewsBasedOnLikes()
        {
            return _repository.GetAllReviews().OrderByDescending(review => review.Likes).ToList();
        }

        public IReadOnlyList<Review> GetOnlyFirstNumberOfReviews(int number)
        {
            return GetReviewsBasedOnLikes().Take(number).ToList();
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
    }
}
