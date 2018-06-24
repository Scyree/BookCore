using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class FollowUserRepository : IFollowUserRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public FollowUserRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public List<FollowUser> GetAllFollowedPeople(Guid userId)
        {
            return _databaseService.FollowUsers.Where(user => user.UserId == userId).ToList();
        }

        public List<FollowUser> GetAllFollowers(Guid userId)
        {
            return _databaseService.FollowUsers.Where(user => user.FollowId == userId).ToList();
        }

        public List<FollowUser> GetAllFollowUsers()
        {
            return _databaseService.FollowUsers.ToList();
        }
        
        public List<FollowUser> GetAllFollowUsersWhereUserIdAppears(Guid userId)
        {
            return _databaseService.FollowUsers.Where(user => user.FollowId == userId || user.UserId == userId).ToList();
        }

        public FollowUser GetFollowUserById(Guid id)
        {
            return _databaseService.FollowUsers.SingleOrDefault(followUser => followUser.Id == id);
        }

        public void CreateFollowUser(FollowUser followUser)
        {
            _databaseService.FollowUsers.Add(followUser);

            _databaseService.SaveChanges();
        }

        public void EditFollowUser(FollowUser followUser)
        {
            _databaseService.FollowUsers.Update(followUser);

            _databaseService.SaveChanges();
        }

        public void DeleteFollowUser(FollowUser followUser)
        {
            _databaseService.FollowUsers.Remove(followUser);

            _databaseService.SaveChanges();
        }
    }
}
