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
        private readonly INotificationRepository _notificationRepository;

        public ApplicationFollowLogic(IApplicationUserRepository applicationRepository, IFollowUserRepository followUserRepository, IFollowUserMiddleware followUserMiddleware, INotificationRepository notificationRepository)
        {
            _applicationRepository = applicationRepository;
            _followUserRepository = followUserRepository;
            _followUserMiddleware = followUserMiddleware;
            _notificationRepository = notificationRepository;
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
                        var notification = Notification.CreateNotification(Guid.Parse(followedId), user.User + " started following you!");

                        _followUserRepository.CreateFollowUser(followUser);
                        _notificationRepository.CreateNotification(notification);
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
                        var notification = Notification.CreateNotification(Guid.Parse(followedId), user.User + " has unfollowed you!");

                        _notificationRepository.CreateNotification(notification);
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

        public IReadOnlyList<ApplicationUser> GetFollowedPeople(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));

            if (user != null)
            {
                var followedUsers = _followUserMiddleware.GetAllFollowedPeople(Guid.Parse(userId));
                var followedPeople = new List<ApplicationUser>();
                
                foreach (var followedUser in followedUsers)
                {
                    followedPeople.Add(_applicationRepository.GetApplicationUserById(followedUser.FollowId));
                }

                return followedPeople;
            }

            return null;
        }

        public IReadOnlyList<ApplicationUser> GetFollowers(string userId)
        {
            var user = _applicationRepository.GetApplicationUserById(Guid.Parse(userId));

            if (user != null)
            {
                var followerUsers = _followUserMiddleware.GetAllFollowers(Guid.Parse(userId));
                var followers = new List<ApplicationUser>();

                foreach (var follower in followerUsers)
                {
                    followers.Add(_applicationRepository.GetApplicationUserById(follower.UserId));
                }

                return followers;
            }

            return null;
        }

        public bool CheckIfAlreadyFollowed(string userId, string followedId)
        {
            return _followUserMiddleware.CheckIfAlreadyFollowed(Guid.Parse(userId), Guid.Parse(followedId));
        }
    }
}
