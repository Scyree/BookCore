﻿using System;
using System.Linq;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ExploreBooks.Models.UserViewModels;

namespace ExploreBooks.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUtilityService _activityService;
        private readonly IApplicationUserServices _service;
        private readonly IApplicationFollowLogic _followLogic;
        private readonly IApplicationBookLogic _bookLogic;
        private readonly IRatingService _ratingService;
        private readonly IChapterService _chapterService;

        public UsersController(IUtilityService activityService, IApplicationUserServices service, IApplicationFollowLogic followLogic, IApplicationBookLogic bookLogic, IRatingService ratingService, IChapterService chapterService)
        {
            _activityService = activityService;
            _service = service;
            _followLogic = followLogic;
            _bookLogic = bookLogic;
            _ratingService = ratingService;
            _chapterService = chapterService;
        }

        [HttpGet, ActionName("allusers")]
        public IActionResult AllUsers()
        {
            return View(_service.GetAllApplicationUsers());
        }

        [HttpGet("{username}")]
        public IActionResult Index(string username)
        {
            var user = _service.GetApplicationUserByUsername(username);
            if (user == null)
            {
                return RedirectToAction("UserNotFound", "Errors");
            }
            
            var model = new ActivityViewModel
            {
                Id = user.Id,
                Username = user.User,
                BookActivity = _activityService.GetAllBooksForUserId(user.Id)
            };

            return View(model);
        }

        [HttpGet("{username}/library")]
        public IActionResult Library(string username)
        {
            var user = _service.GetApplicationUserByUsername(username);
            if (user == null)
            {
                return RedirectToAction("UserNotFound", "Errors");
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
                return RedirectToAction("UserNotFound", "Errors");
            }
            
            var model = new SocialViewModel
            {
                Id = user.Id,
                Username = user.User
            };

            return View(model);
        }

        [HttpPost("Create")]
        public IActionResult Create(Guid bookId, string userId, string actionName)
        {
            _bookLogic.ReadActions(bookId, userId, actionName);
            
            return RedirectToAction("Details", "Books", new { @id = bookId });
        }
        
        [HttpPost("RateBook")]
        public IActionResult RateBook(Guid bookId, string userId, double value)
        {
            _ratingService.RateBook(bookId, userId, value);

            return RedirectToAction("Details", "Books", new { @id = bookId });
        }

        [HttpPost("ChapterBook")]
        public IActionResult ChapterBook(Guid bookId, string userId, string chapters)
        {
            _chapterService.ChapterBook(bookId, userId, chapters);

            return RedirectToAction("Details", "Books", new { @id = bookId });
        }

        [HttpPost("ModifyBookPages")]
        public IActionResult ModifyBookPages(Guid bookId, string userId, string numberOfPages)
        {
            _bookLogic.ModifyBookPages(bookId, userId, numberOfPages);

            return RedirectToAction("Details", "Books", new { @id = bookId });
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

        [HttpPost("DeleteNotifications")]
        public void DeleteNotifications(string userId)
        {
            _activityService.DeleteAllNotificationsForUser(userId);
        }
    }
}
