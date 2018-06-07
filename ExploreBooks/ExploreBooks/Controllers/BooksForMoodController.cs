using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using ExploreBooks.Models.BooksForMoodViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    [Route("listOfBooks")]
    public class BooksForMoodController : Controller
    {
        private readonly IBooksForMoodService _booksForMoodService;

        public BooksForMoodController(IBooksForMoodService booksForMoodService)
        {
            _booksForMoodService = booksForMoodService;
        }
        
        [HttpGet, ActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        [HttpGet("create"), ActionName("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Authors, Genres, Title, Description, Details, Image")] BooksForMoodCreateModel booksCreateModel)
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