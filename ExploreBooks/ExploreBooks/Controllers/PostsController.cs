using System;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _service;

        public PostsController(IPostService service)
        {
            _service = service;
        }
        
        public IActionResult GetAllPostsForTargetId(Guid targetId)
        {
            return PartialView("PartialViews/_PostsDisplay", _service.GetAllPostsForTargetId(targetId));
        }

        [HttpPost]
        public void Create(Guid userId, Guid targetId, string postText)
        {
            _service.CreatePost(userId, targetId, postText);
        }

        [HttpPost]
        public void Edit(Guid postId, string postEdited)
        {
            _service.EditPost(postId, postEdited);
        }
        
        [HttpPost]
        public void Delete(Guid id)
        {
            _service.DeletePost(id);
        }
    }
}
