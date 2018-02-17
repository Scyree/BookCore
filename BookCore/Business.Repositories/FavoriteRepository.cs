using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Persistence;

namespace Business.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public FavoriteRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Favorite> GetAllFavorites()
        {
            return _databaseService.Favorites.ToList();
        }

        public Favorite GetFavoriteById(Guid id)
        {
            return _databaseService.Favorites.SingleOrDefault(favorite => favorite.Id == id);
        }

        public void CreateFavorite(Favorite favorite)
        {
            _databaseService.Favorites.Add(favorite);

            _databaseService.SaveChanges();
        }

        public void EditFavorite(Favorite favorite)
        {
            _databaseService.Favorites.Update(favorite);

            _databaseService.SaveChanges();
        }

        public void DeleteFavorite(Favorite favorite)
        {
            _databaseService.Favorites.Remove(favorite);

            _databaseService.SaveChanges();
        }
    }
}
