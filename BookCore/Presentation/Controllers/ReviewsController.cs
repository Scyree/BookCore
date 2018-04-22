using System;
using Microsoft.AspNetCore.Mvc;
using Data.Domain.Interfaces.Services;
using Presentation.Models.ReviewViewModels;

namespace Presentation.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
        {
            _service = service;
        }

        // GET: Reviews
        public IActionResult Index()
        {
        
            return View(_service.GetAllReviews());
        }

        // GET: Reviews/Details
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _service.GetReviewById(id.Value);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserId, BookId, Description, BookRating")] ReviewCreateModel reviewCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(reviewCreateModel);
            }

            _service.CreateReview(reviewCreateModel.UserId, reviewCreateModel.BookId, reviewCreateModel.Description, reviewCreateModel.BookRating);

            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Edit
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _service.GetReviewById(id.Value);

            if (review == null)
            {
                return NotFound();
            }

            var reviewEditModel = new ReviewEditModel(
                review.Description,
                review.BookRating
            );

            return View(reviewEditModel);
        }

        // POST: Reviews/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Description, BookRating")] ReviewEditModel reviewEditModel)
        {
            if (!ModelState.IsValid)
            {
                return View(reviewEditModel);
            }

            _service.EditReview(id, reviewEditModel.Description, reviewEditModel.BookRating);

            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Delete
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = _service.GetReviewById(id.Value);

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
            _service.DeleteReview(id);

            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Upvote(Guid reviewId, Guid userId)
        {
            _service.UpvoteReview(reviewId, userId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Downvote(Guid reviewId, Guid userId)
        {
            _service.DownvoteReview(reviewId, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
