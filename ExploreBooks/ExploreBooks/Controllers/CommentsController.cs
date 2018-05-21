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

        public IActionResult GetAllCommentsForUserId(Guid postId)
        {
            return PartialView("PartialViews/_CommentsDisplay", _service.GetAllComments(postId));
        }

        [HttpPost]
        public void Create(Guid userId, Guid postId, string commentText)
        {
            _service.CreateComment(userId, postId, commentText);
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
    }
}
