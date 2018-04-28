using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IRatingRepository
    {
        IReadOnlyList<Rating> GetAllRatings();
        Rating GetRatingById(Guid id);
        void CreateRating(Rating rating);
        void EditRating(Rating rating);
        void DeleteRating(Rating rating);
    }
}
