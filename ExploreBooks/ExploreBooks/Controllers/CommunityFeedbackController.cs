using System;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    public class CommunityFeedbackController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public CommunityFeedbackController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }
        
        public IActionResult GetAllPostsForTargetId(Guid targetId)
        {
            return PartialView("PartialViews/_PostsDisplay", _postService.GetAllPostsForTargetId(targetId));
        }

        public IActionResult GetAllCommentsForPostGivenCommentId(Guid commentId)
        {
            return PartialView("PartialViews/_CommentsDisplay", _commentService.GetAllComentsForThePostGivenCommentId(commentId));
        }

        public IActionResult GetAllCommentsForPostId(Guid postId)
        {
            return PartialView("PartialViews/_CommentsDisplay", _commentService.GetAllComments(postId));
        }

        [HttpPost]
        public void CreatePost(Guid userId, Guid targetId, string postText)
        {
            _postService.CreatePost(userId, targetId, postText);
        }

        [HttpPost]
        public void CreateComment(Guid userId, Guid postId, string commentText)
        {
            _commentService.CreateComment(userId, postId, commentText);
        }
        
        [HttpPost]
        public void EditPost(Guid postId, string postEdited)
        {
            _postService.EditPost(postId, postEdited);
        }

        [HttpPost]
        public void EditComment(Guid commentId, string commentEdited)
        {
            _commentService.EditComment(commentId, commentEdited);
        }

        [HttpPost]
        public void DeletePost(Guid id)
        {
            _postService.DeletePost(id);
        }

        [HttpPost]
        public void DeleteComment(Guid id)
        {
            _commentService.DeleteComment(id);
        }
    }
}
