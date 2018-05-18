using System;
using System.Collections.Generic;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBookStateGeneralUsage _stateService;
        private readonly IApplicationUserServices _service;
        
        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IBookStateGeneralUsage stateService, IApplicationUserServices service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _stateService = stateService;
            _service = service;
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
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                Country = user.Country
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guid bookId, string userId, string actionName)
        {
            _service.ReadActions(bookId, userId, actionName);

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

            user.Books = _stateService.GetAllBookStates().Where(state => state.UserId.ToString() == user.Id).ToList();

            var model = new LibraryViewModel
            {
                Books = user.Books
            };

            return View(model);
        }
    }
}
