using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IRatingRepository
    {
        Task<IReadOnlyList<Rating>> GetAllRatings();
        Task<Rating> GetRatingById(Guid id);
        Task CreateRating(Rating rating);
        Task EditRating(Rating rating);
        Task DeleteRating(Rating rating);
    }
}
