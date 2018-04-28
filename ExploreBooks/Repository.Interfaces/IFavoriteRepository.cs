using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
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
