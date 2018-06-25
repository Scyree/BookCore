﻿using System;
using Business.Interfaces;
using ExploreBooks.Models.BookListViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    [Route("listOfBooks/[action]")]
    public class BookListsController : Controller
    {
        private readonly IBookListService _booksForMoodService;

        public BookListsController(IBookListService booksForMoodService)
        {
            _booksForMoodService = booksForMoodService;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(_booksForMoodService.GetAllBookLists());
        }

        [HttpGet, ActionName("details")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("ObjectNotFound", "Errors");
            }

            var book = _booksForMoodService.GetBookListById(id.Value);

            if (book == null)
            {
                return RedirectToAction("ObjectNotFound", "Errors");
            }

            return View(book);
        }

        [HttpGet, ActionName("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookListCreateModel booksCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(booksCreateModel);
            }
            
            _booksForMoodService.CreateBookList(Guid.Parse(booksCreateModel.UserId), booksCreateModel.Title, booksCreateModel.Description, booksCreateModel.Books);

            return RedirectToAction(nameof(Index));
        }
        
    }
}