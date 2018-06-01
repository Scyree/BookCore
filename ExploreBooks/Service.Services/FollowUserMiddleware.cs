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

        public IReadOnlyList<FollowUser> GetAllFollowedPeople(Guid userId)
        {
           return _repository.GetAllFollowUsers().Where(user => user.UserId == userId).ToList();
        }

        public IReadOnlyList<FollowUser> GetAllFollowers(Guid userId)
        {
            return _repository.GetAllFollowUsers().Where(user => user.FollowId == userId).ToList();
        }

        public bool CheckIfAlreadyFollowed(Guid userId, Guid followedId)
        {
            return _repository.GetAllFollowUsers().Any(user => user.UserId == userId && user.FollowId == followedId);
        }
    }
}
