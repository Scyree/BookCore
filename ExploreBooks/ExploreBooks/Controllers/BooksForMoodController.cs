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

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

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