using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class BuyingSiteRepository : IBuyingSiteRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public BuyingSiteRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IReadOnlyList<BuyingSite>> GetAllBuyingSites()
        {
            return await _databaseService.BuyingSites.ToListAsync();
        }

        public async Task<BuyingSite> GetBuyingSiteById(Guid id)
        {
            return await _databaseService.BuyingSites.SingleOrDefaultAsync(buyingSite => buyingSite.Id == id);
        }

        public async Task CreateBuyingSite(BuyingSite buyingSite)
        {
            _databaseService.BuyingSites.Add(buyingSite);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditBuyingSite(BuyingSite buyingSite)
        {
            _databaseService.BuyingSites.Update(buyingSite);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteBuyingSite(BuyingSite buyingSite)
        {
            _databaseService.BuyingSites.Remove(buyingSite);

            await _databaseService.SaveChangesAsync();
        }
    }
}
