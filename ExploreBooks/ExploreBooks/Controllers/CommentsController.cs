using System;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ExploreBooks.Models.CommentViewModels;

namespace ExploreBooks.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService service)
        {
            _service = service;
        }

        // GET: Comments
        public IActionResult Index(Guid targetId)
        {
            return View(_service.GetAllComments(targetId));
        }

        // GET: Comments/Details
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _service.GetCommentById(id.Value);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create([Bind("UserId, TargetId, Text")] CommentCreateModel commentCreateModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(commentCreateModel);
        //    }

        //    _service.CreateComment(commentCreateModel.UserId, commentCreateModel.TargetId, commentCreateModel.Text);

        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guid userId, Guid targetId, string commentText)
        {
            _service.CreateComment(userId, targetId, commentText);

            return RedirectToAction(nameof(Index));
        }

        // GET: Comments/Edit
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _service.GetCommentById(id.Value);

            if (comment == null)
            {
                return NotFound();
            }

            var commentEditModel = new CommentEditModel(
                comment.Text
            );

            return View(commentEditModel);
        }

        // POST: Comments/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Text")] CommentEditModel commentEditModel)
        {
            if (!ModelState.IsValid)
            {
                return View(commentEditModel);
            }

            _service.EditComment(id, commentEditModel.Text);

            return RedirectToAction(nameof(Index));
        }

        // GET: Comments/Delete
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _service.GetCommentById(id.Value);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteComment(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Upvote(Guid commentId, Guid userId)
        {
            _service.UpvoteComment(commentId, userId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Downvote(Guid commentId, Guid userId)
        {
            _service.DownvoteComment(commentId, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
