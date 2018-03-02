using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Services;
using Presentation.Models.CommentViewModels;

namespace Presentation.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _repository;

        public CommentsController(ICommentService repository)
        {
            _repository = repository;
        }

        // GET: Comments
        public IActionResult Index()
        {
            return View(_repository.GetAllComments());
        }

        // GET: Comments/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _repository.GetCommentById(id.Value);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserId, TargetId, Text")] CommentCreateModel commentCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(commentCreateModel);
            }

            _repository.CreateComment(
                Comment.CreateComment(
                    commentCreateModel.UserId,
                    commentCreateModel.TargetId,
                    commentCreateModel.Text
                )
            );

            return RedirectToAction(nameof(Index));
        }

        // GET: Comments/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _repository.GetCommentById(id.Value);
            if (comment == null)
            {
                return NotFound();
            }

            var commentEditModel = new CommentEditModel(
                comment.UserId,
                comment.TargetId,
                comment.Text
            );

            return View(commentEditModel);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("UserId, TargetId, Text")] CommentEditModel commentEditModel)
        {
            var commentToBeEdited = _repository.GetCommentById(id);

            if (commentToBeEdited == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(commentToBeEdited);
            }

            commentToBeEdited.UserId = commentEditModel.UserId;
            commentToBeEdited.TargetId = commentEditModel.TargetId;
            commentToBeEdited.Text = commentEditModel.Text;
        
            try
            {
                _repository.EditComment(commentToBeEdited);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(_repository.GetCommentById(id).Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Comments/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _repository.GetCommentById(id.Value);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var commentToBeDeleted = _repository.GetCommentById(id);
            _repository.DeleteComment(commentToBeDeleted);

            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(Guid id)
        {
            return _repository.GetAllComments().Any(e => e.Id == id);
        }
    }
}
