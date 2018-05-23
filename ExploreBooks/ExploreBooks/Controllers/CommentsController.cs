using System;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _service;


        public CommentsController(ICommentService service)
        {
            _service = service;
        }
        
        public IActionResult GetAllCommentsForUserId(Guid postId)
        {
            return PartialView("PartialViews/_CommentsDisplay", _service.GetAllComments(postId));
        }

        [HttpPost]
        public void Create(Guid userId, Guid postId, string commentText)
        {
            _service.CreateComment(userId, postId, commentText);
        }
        
        [HttpPost]
        public void Edit(Guid postId, string commentEdited)
        {
            _service.EditComment(postId, commentEdited);
        }
        
        [HttpPost]
        public void Delete(Guid id)
        {
            _service.DeleteComment(id);
        }
    }
}
