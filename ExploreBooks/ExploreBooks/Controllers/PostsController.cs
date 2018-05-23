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
        
        public IActionResult GetAllPostsForUserId(Guid targetId)
        {
            return PartialView("PartialViews/_PostsDisplay", _service.GetAllPostsForTargetId(targetId));
        }

        [HttpPost]
        public void Create(Guid userId, Guid targetId, string postText)
        {
            _service.CreatePost(userId, targetId, postText);
        }
        
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Guid id, PostEditModel reviewEditModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(reviewEditModel);
        //    }

        //    _service.EditPost(id, reviewEditModel.Description);

        //    return RedirectToAction();
        //}
        
        [HttpPost]
        public void Delete(Guid id)
        {
            _service.DeletePost(id);
        }
    }
}
