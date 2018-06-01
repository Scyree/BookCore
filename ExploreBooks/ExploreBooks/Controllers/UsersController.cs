using System;
using System.Linq;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Data;
using ExploreBooks.Models.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Service.Interfaces;

namespace ExploreBooks.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookStateMiddleware _stateService;
        private readonly IUtilityService _activityService;
        private readonly IApplicationUserServices _service;
        private readonly IApplicationFollowLogic _followLogic;
        private readonly IApplicationBookLogic _bookLogic;
        private readonly IPostService _postService;

        public UsersController(UserManager<ApplicationUser> userManager, IUtilityService activityService, IBookStateMiddleware stateService, IApplicationUserServices service, IPostService postService, IApplicationFollowLogic followLogic, IApplicationBookLogic bookLogic)
        {
            _userManager = userManager;
            _activityService = activityService;
            _stateService = stateService;
            _service = service;
            _postService = postService;
            _followLogic = followLogic;
            _bookLogic = bookLogic;
        }
        
        [HttpGet("{username}")]
        public IActionResult Index(string username)
        {
            var user = _service.GetApplicationUserByUsername(username);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            var model = new IndexViewModel
            {
                Id = user.Id,
                Username = user.User,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                Country = user.Country,
                BookActivity = _activityService.GetAllBooksForUserId(user.Id)
            };

            return View(model);
        }

        [HttpPost("Create")]
        public IActionResult Create(Guid bookId, string userId, string actionName)
        {
            _bookLogic.ReadActions(bookId, userId, actionName);
            
            return RedirectToAction("Details", "Books", new { @id = bookId });
        }
        
        [HttpGet("{username}/library")]
        public IActionResult Library(string username)
        {
            var user = _service.GetApplicationUserByUsername(username);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new LibraryViewModel
            {
                Id = user.Id,
                Username = user.User,
                Books = _bookLogic.GetBooksOfAUser(user.Id).ToList()
            };

            return View(model);
        }

        [HttpGet("{username}/social")]
        public IActionResult Social(string username)
        {
            var user = _service.GetApplicationUserByUsername(username);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.Posts = _postService.GetAllPostsForTargetId(Guid.Parse(user.Id)).ToList();
            user.Books = _stateService.GetFavoriteBookStatesByUserId(Guid.Parse(user.Id)).ToList();

            return View(user);
        }

        [HttpPost("AddToFavorites")]
        public IActionResult AddToFavorites(Guid bookId, string userId)
        {
            _bookLogic.AddToFavorites(bookId, userId);

            return RedirectToAction("Details", "Books", new {@id = bookId});
        }

        [HttpPost("RemoveFromFavorites")]
        public IActionResult RemoveFromFavorites(Guid bookId, string userId)
        {
            _bookLogic.RemoveFromFavorites(bookId, userId);

            return RedirectToAction("Details", "Books", new { @id = bookId });
        }

        [HttpPost("FollowUser")]
        public void FollowUser(string userId, string followedId)
        {
            _followLogic.FollowUser(userId, followedId);
        }

        [HttpPost("UnfollowUser")]
        public void UnfollowUser(string userId, string followedId)
        {
            _followLogic.UnfollowUser(userId, followedId);
        }
    }
}
