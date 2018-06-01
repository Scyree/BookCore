using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Microsoft.AspNetCore.Http;

namespace Business.Interfaces
{
    public interface IApplicationUserServices
    {
        IReadOnlyList<ApplicationUser> GetAllApplicationUsers();
        ApplicationUser GetApplicationUserByUsername(string username);
        ApplicationUser GetApplicationUserById(Guid id);
        void CreateApplicationUser(ApplicationUser applicationUser);
        void EditApplicationUser(ApplicationUser applicationUser);
        void DeleteApplicationUser(ApplicationUser applicationUser);
    }
}
