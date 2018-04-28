using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
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
