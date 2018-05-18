using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IReadOnlyList<Favorite>> GetAllFavorites();
        Task<Favorite> GetFavoriteById(Guid id);
        Task CreateFavorite(Favorite favorite);
        Task EditFavorite(Favorite favorite);
        Task DeleteFavorite(Favorite favorite);
    }
}
