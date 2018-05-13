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
            var user = _databaseService.Users.FirstOrDefault(usr => usr.Id == id.ToString());
            return user.FirstName + " " + user.LastName;
        }

    }
}
