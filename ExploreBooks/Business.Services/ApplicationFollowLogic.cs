using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class ApplicationFollowLogic : IApplicationFollowLogic
    {
        private readonly IFollowUserMiddleware _followUserMiddleware;
        private readonly IFollowUserRepository _followUserRepository;
        private readonly IApplicationUserRepository _applicationRepository;

        public ApplicationFollowLogic(IApplicationUserRepository applicationRepository, IFollowUserRepository followUserRepository, IFollowUserMiddleware followUserMiddleware)
        {
            _applicationRepository = applicationRepository;
            _followUserRepository = followUserRepository;
            _followUserMiddleware = followUserMiddleware;
        }

        public void FollowUser(string userId, string followedId)
        {
            if (userId != followedId)
            {
                var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
                var followed = _applicationRepository.GetApplicationUserById(Guid.Parse(followedId));

                if (user != null && followed != null)
                {
                    var checkIfAlreadyFollowed = _followUserMiddleware.CheckIfAlreadyFollowed(Guid.Parse(userId), Guid.Parse(followedId));

                    if (!checkIfAlreadyFollowed)
                    {
                        var followUser = Domain.Data.FollowUser.CreateFollowUser(Guid.Parse(userId), Guid.Parse(followedId));

                        _followUserRepository.CreateFollowUser(followUser);
                    }
                }
            }
        }

        public void UnfollowUser(string userId, string followedId)
        {
            if (userId != followedId)
            {
                var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
                var followed = _applicationRepository.GetApplicationUserById(Guid.Parse(followedId));

                if (user != null && followed != null)
                {
                    var followedContent = _followUserRepository.GetAllFollowUsers().SingleOrDefault(followUser => followUser.UserId == Guid.Parse(userId) && followUser.FollowId == Guid.Parse(followedId));

                    if (followedContent != null)
                    {
                       _followUserRepository.DeleteFollowUser(followedContent);
                    }
                }
            }
        }

        public int GetNumberOfFollowedPeople(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var count = 0;

            if (user != null)
            {
                count += _followUserMiddleware.GetAllFollowedPeople(Guid.Parse(userId)).Count;

                return count;
            }

            return count;
        }

        public int GetNumberOfFollowers(string userId)
        {
            var searchedUser = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));
            var count = 0;

            if (searchedUser != null)
            {
                count += _followUserMiddleware.GetAllFollowers(Guid.Parse(userId)).Count;
            }

            return count;
        }

        public IReadOnlyList<FollowUser> GetFollowedPeople(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));

            if (user != null)
            {
                var followedPeople = _followUserMiddleware.GetAllFollowedPeople(Guid.Parse(userId));

                return followedPeople;
            }

            return null;
        }

        public bool CheckIfAlreadyFollowed(string userId, string followedId)
        {
            return _followUserMiddleware.CheckIfAlreadyFollowed(Guid.Parse(userId), Guid.Parse(followedId));
        }
    }
}
