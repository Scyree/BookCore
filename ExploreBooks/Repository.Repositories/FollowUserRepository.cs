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

        public IReadOnlyList<FollowUser> GetAllFollowUsers()
        {
            return _databaseService.FollowUsers.ToList();
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
