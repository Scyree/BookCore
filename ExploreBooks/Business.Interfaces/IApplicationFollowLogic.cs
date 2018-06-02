using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IApplicationFollowLogic
    {
        void FollowUser(string userId, string followedId);
        void UnfollowUser(string userId, string followedId);
        int GetNumberOfFollowedPeople(string userId);
        int GetNumberOfFollowers(string userId);
        IReadOnlyList<ApplicationUser> GetFollowedPeople(string userId);
        IReadOnlyList<ApplicationUser> GetFollowers(string userId);
        bool CheckIfAlreadyFollowed(string userId, string followedId);
    }
}
