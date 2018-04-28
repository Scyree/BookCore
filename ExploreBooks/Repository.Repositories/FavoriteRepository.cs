using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
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
