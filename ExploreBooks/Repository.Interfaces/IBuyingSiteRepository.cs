using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IBuyingSiteRepository
    {
        Task<IReadOnlyList<BuyingSite>> GetAllBuyingSites();
        Task<BuyingSite> GetBuyingSiteById(Guid id);
        Task CreateBuyingSite(BuyingSite buyingSite);
        Task EditBuyingSite(BuyingSite buyingSite);
        Task DeleteBuyingSite(BuyingSite buyingSite);
    }
}
