using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IFollowUserMiddleware
    {
        IReadOnlyList<FollowUser> GetAllFollowedPeople(Guid userId);
        IReadOnlyList<FollowUser> GetAllFollowers(Guid userId);
        bool CheckIfAlreadyFollowed(Guid userId, Guid followedId);
    }
}
