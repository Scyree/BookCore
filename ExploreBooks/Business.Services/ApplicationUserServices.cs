using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class ApplicationUserServices : IApplicationUserServices
    {
        private readonly IApplicationUserRepository _applicationRepository;
        private readonly IPostService _postService;
        private readonly IFollowUserMiddleware _followUserMiddleware;
        private readonly IPostMiddleware _postMiddleware;
        private readonly IBookStateMiddleware _bookStateMiddleware;
        private readonly ICommentMiddleware _commentMiddleware;
        private readonly ILikeService _likeService;

        public ApplicationUserServices(IApplicationUserRepository applicationRepository, IPostService postService, IFollowUserMiddleware followUserMiddleware, IPostMiddleware postMiddleware, IBookStateMiddleware bookStateMiddleware, ICommentMiddleware commentMiddleware, ILikeService likeService)
        {
            _applicationRepository = applicationRepository;
            _postService = postService;
            _followUserMiddleware = followUserMiddleware;
            _postMiddleware = postMiddleware;
            _bookStateMiddleware = bookStateMiddleware;
            _commentMiddleware = commentMiddleware;
            _likeService = likeService;
        }

        public IReadOnlyList<ApplicationUser> GetAllApplicationUsers()
        {
            var applicationUsers = _applicationRepository.GetAllApplicationUsers();

            foreach (var applicationUser in applicationUsers)
            {
                applicationUser.Following = _followUserMiddleware.GetAllFollowedPeople(Guid.Parse(applicationUser.Id)).ToList();
                applicationUser.Posts = _postService.GetAllPostsForTargetId(Guid.Parse(applicationUser.Id)).ToList();
            }

            return _applicationRepository.GetAllApplicationUsers();
        }

        public ApplicationUser GetApplicationUserByUsername(string username)
        {
            var applicationUser = _applicationRepository.GetAllApplicationUsers().SingleOrDefault(user => user.User == username);

            if (applicationUser != null)
            {
                applicationUser.Following = _followUserMiddleware.GetAllFollowedPeople(Guid.Parse(applicationUser.Id)).ToList();
                applicationUser.Posts = _postService.GetAllPostsForTargetId(Guid.Parse(applicationUser.Id)).ToList();

                return applicationUser;
            }

            return null;
        }

        public ApplicationUser GetApplicationUserById(Guid id)
        {
            var applicationUser = _applicationRepository.GetApplicationUserById(id);

            if (applicationUser != null)
            {
                applicationUser.Following = _followUserMiddleware.GetAllFollowedPeople(Guid.Parse(applicationUser.Id)).ToList();
                applicationUser.Posts = _postService.GetAllPostsForTargetId(Guid.Parse(applicationUser.Id)).ToList();

                return applicationUser;
            }

            return null;
        }

        public void CreateApplicationUser(ApplicationUser applicationUser)
        {
            _applicationRepository.CreateApplicationUser(applicationUser);
        }

        public void EditApplicationUser(ApplicationUser applicationUser)
        {
            _applicationRepository.EditApplicationUser(applicationUser);
        }
        
        public void DeleteApplicationUser(ApplicationUser applicationUser)
        {
            var userId = Guid.Parse(applicationUser.Id);

            _likeService.DeleteUserLikes(userId);
            _followUserMiddleware.DeleteUserFollow(userId);
            _postMiddleware.DeleteUserPosts(userId);
            _bookStateMiddleware.DeleteUserStates(userId);
            _commentMiddleware.DeleteUserComments(userId);
        }

        public bool CheckIfUsernameAlreadyExists(string username)
        {
            return _applicationRepository.GetAllApplicationUsers().Any(user => user.User == username);
        }
    }
}
