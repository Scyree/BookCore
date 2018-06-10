using System;
using Business.Interfaces;
using ExploreBooks.Models.BooksForMoodViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    [Route("listOfBooks/[action]")]
    public class BooksForMoodController : Controller
    {
        private readonly IBooksForMoodService _booksForMoodService;

        public BooksForMoodController(IBooksForMoodService booksForMoodService)
        {
            _booksForMoodService = booksForMoodService;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(_booksForMoodService.GetAllBooksForMoods());
        }

        [HttpGet, ActionName("details")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _booksForMoodService.GetBooksForMoodById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BooksForMoodCreateModel booksCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(booksCreateModel);
            }

            _booksForMoodService.CreateBooksForMood(Guid.Parse(booksCreateModel.UserId), booksCreateModel.Title, booksCreateModel.Description, booksCreateModel.Books);

            return RedirectToAction(nameof(Index));
        }
        
    }
}