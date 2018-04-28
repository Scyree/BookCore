using System.Linq;
using Business.Interfaces;
using Domain.Persistence;

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
