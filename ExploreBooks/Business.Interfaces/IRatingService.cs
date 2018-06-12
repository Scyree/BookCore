using System;
using System.Collections.Generic;
using Domain.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Business.Interfaces
{
    public interface IRatingService
    {
        bool CheckIfUserRatedThisBook(Guid userId, Guid bookId);
        Rating GetUserRatingForBook(Guid userId, Guid bookId);
        IReadOnlyList<Rating> GetAllRatingsForBook(Guid bookId);
        double GetRatingsAverageForBook(Guid bookId);
        List<SelectListItem> GetRatingList();
        void RateBook(Guid bookId, string userId, double value);
        string ConvertRateToText(double rate);
    }
}
