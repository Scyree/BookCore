using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IRecommandationRepository
    {
        Task<IReadOnlyList<Recommandation>> GetAllRecommandations();
        Task<Recommandation> GetRecommandationById(Guid id);
        Task CreateRecommandation(Recommandation recommandation);
        Task EditRecommandation(Recommandation recommandation);
        Task DeleteRecommandation(Recommandation recommandation);
    }
}
