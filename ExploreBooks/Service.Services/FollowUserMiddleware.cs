using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class FollowUserMiddleware : IFollowUserMiddleware
    {
        private readonly IFollowUserRepository _repository;

        public FollowUserMiddleware(IFollowUserRepository repository)
        {
            _repository = repository;
        }

        public List<FollowUser> GetAllFollowedPeople(Guid userId)
        {
            return _repository.GetAllFollowedPeople(userId);
        }

        public List<FollowUser> GetAllFollowers(Guid userId)
        {
            return _repository.GetAllFollowers(userId);
        }

        public bool CheckIfAlreadyFollowed(Guid userId, Guid followedId)
        {
            return _repository.GetAllFollowUsers().Any(user => user.UserId == userId && user.FollowId == followedId);
        }

        public void DeleteUserFollow(Guid userId)
        {
            var followers = _repository.GetAllFollowUsersWhereUserIdAppears(userId);

            foreach (var follow in followers)
            {
                _repository.DeleteFollowUser(follow);
            }
        }
    }
}
