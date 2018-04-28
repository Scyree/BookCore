using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
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

        public IReadOnlyList<BuyingSite> GetAllBuyingSites()
        {
            return _databaseService.BuyingSites.ToList();
        }

        public BuyingSite GetBuyingSiteById(Guid id)
        {
            return _databaseService.BuyingSites.SingleOrDefault(buyingSite => buyingSite.Id == id);
        }

        public void CreateBuyingSite(BuyingSite buyingSite)
        {
            _databaseService.BuyingSites.Add(buyingSite);

            _databaseService.SaveChanges();
        }

        public void EditBuyingSite(BuyingSite buyingSite)
        {
            _databaseService.BuyingSites.Update(buyingSite);

            _databaseService.SaveChanges();
        }

        public void DeleteBuyingSite(BuyingSite buyingSite)
        {
            _databaseService.BuyingSites.Remove(buyingSite);

            _databaseService.SaveChanges();
        }
    }
}
