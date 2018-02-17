using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Persistence;

namespace Business.Repositories
{
    public class DetailRepository : IDetailRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public DetailRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Detail> GetAllDetails()
        {
            return _databaseService.Details.ToList();
        }

        public Detail GetDetailById(Guid id)
        {
            return _databaseService.Details.SingleOrDefault(detail => detail.Id == id);
        }

        public void CreateDetail(Detail detail)
        {
            _databaseService.Details.Add(detail);

            _databaseService.SaveChanges();
        }

        public void EditDetail(Detail detail)
        {
            _databaseService.Details.Update(detail);

            _databaseService.SaveChanges();
        }

        public void DeleteDetail(Detail detail)
        {
            _databaseService.Details.Remove(detail);

            _databaseService.SaveChanges();
        }
    }
}
