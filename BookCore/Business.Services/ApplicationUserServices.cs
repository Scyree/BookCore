using Data.Domain.Interfaces.Services;
using Data.Persistance;
using System;
using System.Linq;

namespace Business.Services
{
    public class ApplicationUserServices : IApplicationUserServices
    {
        private readonly ApplicationDbContext _databaseService;

        public ApplicationUserServices(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public string GetNameOfTheSpecifiedId(string id)
        {
            return _databaseService.Users.FirstOrDefault(user => user.Id == id.ToString()).Name;
        }

    }
}
