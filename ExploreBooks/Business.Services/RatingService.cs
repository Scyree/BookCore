using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Interfaces;

namespace Business.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _repository;

        public RatingService(IRatingRepository repository)
        {
            _repository = repository;
        }

        public bool CheckIfUserRatedThisBook(Guid userId, Guid bookId)
        {
            return _repository.GetAllRatings().Any(rate => rate.UserId == userId && rate.BookId == bookId);
        }

        public Rating GetUserRatingForBook(Guid userId, Guid bookId)
        {
            return _repository.GetAllRatings().SingleOrDefault(rate => rate.UserId == userId && rate.BookId == bookId);
        }

        public IReadOnlyList<Rating> GetAllRatingsForBook(Guid bookId)
        {
            return _repository.GetAllRatings().Where(rate => rate.BookId == bookId).ToList();
        }

        public double GetRatingsAverageForBook(Guid bookId)
        {
            var rates = GetAllRatingsForBook(bookId);
            var average = 5.0;

            if (rates.Count > 0)
            {
                var sum = 0.0;

                foreach (var rate in rates)
                {
                    sum += rate.Rate;
                }

                average = sum / rates.Count;
            }
            
            return Math.Round(average, 2);
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

        public string ConvertRateToText(double rate)
        {
            switch (rate)
            {
                case 1:
                    return "Hate it";
                case 2:
                    return "Not impressed";
                case 3:
                    return "Normal";
                case 4:
                    return "Good";
                default:
                    return "Very good";
            }
        }

        public void RateBook(Guid bookId, string userId, double value)
        {
            var check = CheckIfUserRatedThisBook(Guid.Parse(userId), bookId);

            if (!check)
            {
                var rate = Rating.CreateRating(Guid.Parse(userId), bookId);
                rate.Rate = value;

                _repository.CreateRating(rate);
            }
            else
            {
                var rate = GetUserRatingForBook(Guid.Parse(userId), bookId);
                rate.Rate = value;

                _repository.EditRating(rate);
            }
        }
    }
}
