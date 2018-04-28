using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
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
