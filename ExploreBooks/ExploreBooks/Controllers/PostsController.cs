using System;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ExploreBooks.Models.PostViewModels;

namespace ExploreBooks.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _service;

        public PostsController(IPostService service)
        {
            _service = service;
        }

        // GET: Reviews
        public IActionResult Index()
        {
        
            return View(_service.GetAllPosts());
        }

        // GET: Reviews/Details
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _service.GetPostById(id.Value);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        public IActionResult GetAllPostsForUserId(Guid targetId)
        {
            return PartialView("PartialViews/_PostsDisplay", _service.GetAllPostsForTargetId(targetId));
        }

        [HttpPost]
        public void Create(Guid userId, Guid targetId, string postText)
        {
            _service.CreatePost(userId, targetId, postText);
        }

        // GET: Reviews/Edit
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _service.GetPostById(id.Value);

            if (review == null)
            {
                return NotFound();
            }

            var reviewEditModel = new PostEditModel(
                review.Description
            );

            return View(reviewEditModel);
        }

        // POST: Reviews/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PostEditModel reviewEditModel)
        {
            if (!ModelState.IsValid)
            {
                return View(reviewEditModel);
            }

            _service.EditPost(id, reviewEditModel.Description);

            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Delete
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _service.GetPostById(id.Value);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeletePost(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
