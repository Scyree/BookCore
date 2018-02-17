using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
{
    public interface IBuyingSiteRepository
    {
        IReadOnlyList<BuyingSite> GetAllBuyingSites();
        BuyingSite GetBuyingSiteById(Guid id);
        void CreateBuyingSite(BuyingSite buyingSite);
        void EditBuyingSite(BuyingSite buyingSite);
        void DeleteBuyingSite(BuyingSite buyingSite);
    }
}
