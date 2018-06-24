using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IFollowUserMiddleware
    {
        List<FollowUser> GetAllFollowedPeople(Guid userId);
        List<FollowUser> GetAllFollowers(Guid userId);
        bool CheckIfAlreadyFollowed(Guid userId, Guid followedId);
        void DeleteUserFollow(Guid userId);
    }
}
