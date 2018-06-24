using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IFollowUserRepository
    {
        List<FollowUser> GetAllFollowedPeople(Guid userId);
        List<FollowUser> GetAllFollowers(Guid userId);
        List<FollowUser> GetAllFollowUsers();
        List<FollowUser> GetAllFollowUsersWhereUserIdAppears(Guid userId);
        FollowUser GetFollowUserById(Guid id);
        void CreateFollowUser(FollowUser followUser);
        void EditFollowUser(FollowUser followUser);
        void DeleteFollowUser(FollowUser followUser);
    }
}
