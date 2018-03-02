using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Services;
using Presentation.Models.ReviewViewModels;

namespace Presentation.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _repository;

        public ReviewsController(IReviewService repository)
        {
            _repository = repository;
        }

        // GET: Reviews
        public IActionResult Index()
        {
        
            return View(_repository.GetReviewsByDate());
        }

        // GET: Reviews/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _repository.GetReviewById(id.Value);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserId, BookId, Description, BookRating")] ReviewCreateModel reviewCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(reviewCreateModel);
            }
            
            _repository.CreateReview(
                Review.CreateReview(
                    reviewCreateModel.BookRating,
                    reviewCreateModel.Description,
                    reviewCreateModel.UserId,
                    reviewCreateModel.BookId
                )
            );

            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _repository.GetReviewById(id.Value);
            if (review == null)
            {
                return NotFound();
            }

            var reviewEditModel = new ReviewEditModel(
                review.UserId,
                review.BookId,
                review.Description,
                review.BookRating
            );

            return View(reviewEditModel);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("UserId, BookId, Description, BookRating")] ReviewEditModel reviewEditModel)
        {
            var reviewToBeEdited = _repository.GetReviewById(id);

            if (reviewToBeEdited == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(reviewEditModel);
            }
            
            reviewToBeEdited.UserId = reviewEditModel.UserId;
            reviewToBeEdited.BookId = reviewEditModel.BookId;
            reviewToBeEdited.Description = reviewEditModel.Description;
            reviewToBeEdited.BookRating = reviewEditModel.BookRating;

            try
            {
                _repository.EditReview(reviewToBeEdited);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(_repository.GetReviewById(id).Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _repository.GetReviewById(id.Value);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var reviewToBeDeleted = _repository.GetReviewById(id);
            _repository.DeleteReview(reviewToBeDeleted);

            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(Guid id)
        {
            return _repository.GetAllReviews().Any(e => e.Id == id);
        }

        public IActionResult Upvote(Guid reviewId, Guid userId)
        {
            _repository.Upvote(reviewId, userId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Downvote(Guid reviewId, Guid userId)
        {
            _repository.Downvote(reviewId, userId);
            //_repository.DeleteNegativeReviews(reviewId);

            return RedirectToAction(nameof(Index));
        }
    }
}
