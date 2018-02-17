using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
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
