﻿using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IApplicationUserServices
    {
        List<ApplicationUser> GetAllApplicationUsers();
        List<ApplicationUser> GetAllApplicationUsersWithinCountry(string userId);
        ApplicationUser GetApplicationUserByUsername(string username);
        ApplicationUser GetApplicationUserById(Guid id);
        void CreateApplicationUser(ApplicationUser applicationUser);
        void EditApplicationUser(ApplicationUser applicationUser);
        void DeleteApplicationUser(ApplicationUser applicationUser);
        bool CheckIfUsernameAlreadyExists(string username);
    }
}
