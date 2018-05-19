using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Microsoft.AspNetCore.Http;

namespace Business.Interfaces
{
    public interface IApplicationUserServices
    {
        string GetNameOfTheSpecifiedId(string userId);
        void CreatePicture(Guid value);
        Task UpdatePicture(string path, IFormFile image);
        string GetFolderWithFile(string userId);
        bool CheckIfHasProfilePicture(string userId);

        List<ApplicationUser> GetAllApplicationUsers();
        ApplicationUser GetApplicationUserById(Guid id);
        void CreateApplicationUser(ApplicationUser applicationUser);
        void EditApplicationUser(ApplicationUser applicationUser);
        void DeleteApplicationUser(ApplicationUser applicationUser);

        void ReadActions(Guid bookId, string userId, string actionName);
        IEnumerable<Book> GetBooksOfAUser(string userId);
        void AddToFavorites(Guid bookId, string userId);
        void RemoveFromFavorites(Guid bookId, string userId);
    }
}
