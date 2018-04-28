using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class RecommandationRepository : IRecommandationRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public RecommandationRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Recommandation> GetAllRecommandations()
        {
            return _databaseService.Recommandations.ToList();
        }

        public Recommandation GetRecommandationById(Guid id)
        {
            return _databaseService.Recommandations.SingleOrDefault(recommandation => recommandation.Id == id);
        }

        public void CreateRecommandation(Recommandation recommandation)
        {
            _databaseService.Recommandations.Add(recommandation);

            _databaseService.SaveChanges();
        }

        public void EditRecommandation(Recommandation recommandation)
        {
            _databaseService.Recommandations.Update(recommandation);

            _databaseService.SaveChanges();
        }

        public void DeleteRecommandation(Recommandation recommandation)
        {
            _databaseService.Recommandations.Remove(recommandation);

            _databaseService.SaveChanges();
        }
    }
}
