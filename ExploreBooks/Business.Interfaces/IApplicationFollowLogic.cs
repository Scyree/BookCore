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
        IReadOnlyList<FollowUser> GetFollowedPeople(string userId);
        bool CheckIfAlreadyFollowed(string userId, string followedId);
    }
}
