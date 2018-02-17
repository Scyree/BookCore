using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
{
    public interface IFavoriteRepository
    {
        IReadOnlyList<Favorite> GetAllFavorites();
        Favorite GetFavoriteById(Guid id);
        void CreateFavorite(Favorite favorite);
        void EditFavorite(Favorite favorite);
        void DeleteFavorite(Favorite favorite);
    }
}
