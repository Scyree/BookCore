using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IReadOnlyList<Favorite>> GetAllFavorites()
        {
            return await _databaseService.Favorites.ToListAsync();
        }

        public async Task<Favorite> GetFavoriteById(Guid id)
        {
            return await _databaseService.Favorites.SingleOrDefaultAsync(favorite => favorite.Id == id);
        }

        public async Task CreateFavorite(Favorite favorite)
        {
            _databaseService.Favorites.Add(favorite);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditFavorite(Favorite favorite)
        {
            _databaseService.Favorites.Update(favorite);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteFavorite(Favorite favorite)
        {
            _databaseService.Favorites.Remove(favorite);

            await _databaseService.SaveChangesAsync();
        }
    }
}
