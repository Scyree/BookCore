using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Data;
using ExploreBooks.Models.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Service.Interfaces;

namespace ExploreBooks.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookStateMiddleware _stateService;
        private readonly IUtilityService _activityService;
        private readonly IApplicationUserServices _service;
        private readonly IPostService _postService;

        public UsersController(UserManager<ApplicationUser> userManager, IUtilityService activityService, IBookStateMiddleware stateService, IApplicationUserServices service, IPostService postService)
        {
            _userManager = userManager;
            _activityService = activityService;
            _stateService = stateService;
            _service = service;
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.User,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                Country = user.Country,
                BookActivity = _activityService.GetAllBooksForUserId(user.Id)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Guid bookId, string userId, string actionName)
        {
            _service.ReadActions(bookId, userId, actionName);

            TempData["ReadAction"] = "Added the book to your " + actionName;

            return RedirectToAction("Details", "Books", new { @id = bookId });
        }

        [HttpGet]
        public async Task<IActionResult> Library()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new LibraryViewModel
            {
                Books = _service.GetBooksOfAUser(user.Id).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> About()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.Posts = _postService.GetAllPostsForTargetId(Guid.Parse(user.Id)).ToList();
            user.Books = _stateService.GetAllBookStatesByUserId(Guid.Parse(user.Id)).ToList();
            //var model = new AboutViewModel
            //{
            //    UserId = user.Id,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Country = user.Country,
            //    Description = user.Description,
            //    Posts = 
            //};

            return View(user);
        }

        public IActionResult AddToFavorites(Guid bookId, string userId)
        {
            _service.AddToFavorites(bookId, userId);

            return RedirectToAction("Details", "Books", new {@id = bookId});
        }

        public IActionResult RemoveFromFavorites(Guid bookId, string userId)
        {
            _service.RemoveFromFavorites(bookId, userId);

            return RedirectToAction("Details", "Books", new { @id = bookId });
        }
        
    }
}
