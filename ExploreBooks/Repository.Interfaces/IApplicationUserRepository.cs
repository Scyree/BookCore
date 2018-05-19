using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IApplicationUserRepository
    {
        IReadOnlyList<ApplicationUser> GetAllApplicationUsers();
        ApplicationUser GetApplicationUserById(Guid id);
        void CreateApplicationUser(ApplicationUser applicationUser);
        void EditApplicationUser(ApplicationUser applicationUser);
        void DeleteApplicationUser(ApplicationUser applicationUser);
    }
}
