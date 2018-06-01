using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IFollowUserRepository
    {
        IReadOnlyList<FollowUser> GetAllFollowUsers();
        FollowUser GetFollowUserById(Guid id);
        void CreateFollowUser(FollowUser followUser);
        void EditFollowUser(FollowUser followUser);
        void DeleteFollowUser(FollowUser followUser);
    }
}
