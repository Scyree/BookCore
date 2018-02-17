using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
{
    public interface IRecommandationRepository
    {
        IReadOnlyList<Recommandation> GetAllRecommandations();
        Recommandation GetRecommandationById(Guid id);
        void CreateRecommandation(Recommandation recommandation);
        void EditRecommandation(Recommandation recommandation);
        void DeleteRecommandation(Recommandation recommandation);
    }
}
