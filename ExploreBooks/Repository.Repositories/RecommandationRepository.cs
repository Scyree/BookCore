using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IReadOnlyList<Recommandation>> GetAllRecommandations()
        {
            return await _databaseService.Recommandations.ToListAsync();
        }

        public async Task<Recommandation> GetRecommandationById(Guid id)
        {
            return await _databaseService.Recommandations.SingleOrDefaultAsync(recommandation => recommandation.Id == id);
        }

        public async Task CreateRecommandation(Recommandation recommandation)
        {
            _databaseService.Recommandations.Add(recommandation);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditRecommandation(Recommandation recommandation)
        {
            _databaseService.Recommandations.Update(recommandation);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteRecommandation(Recommandation recommandation)
        {
            _databaseService.Recommandations.Remove(recommandation);

            await _databaseService.SaveChangesAsync();
        }
    }
}
