using Repository.Interfaces;
using Domain.Data;
using Domain.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Repository.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public ApplicationUserRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<ApplicationUser> GetAllApplicationUsers()
        {
            return _databaseService.Users.ToList();
        }

        public ApplicationUser GetApplicationUserById(Guid id)
        {
            return _databaseService.Users.SingleOrDefault(au => au.Id == id.ToString());
        }

        public void CreateApplicationUser(ApplicationUser applicationUser)
        {
            _databaseService.Users.Add(applicationUser);

            _databaseService.SaveChanges();
        }

        public void EditApplicationUser(ApplicationUser applicationUser)
        {
            _databaseService.Users.Update(applicationUser);

            _databaseService.SaveChanges();
        }

        public void DeleteApplicationUser(ApplicationUser applicationUser)
        {
            _databaseService.Users.Remove(applicationUser);

            _databaseService.SaveChanges();
        }
    }
}
