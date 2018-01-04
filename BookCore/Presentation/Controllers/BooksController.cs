using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Presentation.Models.BookViewModels;

namespace Presentation.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(_repository.GetAllBooks());
        }

        // GET: Books/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _repository.GetBookById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title, Description")] BookCreateModel bookCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookCreateModel);
            }

            _repository.CreateBook(
                Book.CreateBook(
                    bookCreateModel.Title,
                    bookCreateModel.Description
                )
            );

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _repository.GetBookById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            var bookEditModel = new BookEditModel(
                book.Title,
                book.Description
            );

            return View(bookEditModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title, Description")] BookEditModel bookEditModel)
        {
            var bookToBeEdited = _repository.GetBookById(id);

            if (bookToBeEdited == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(bookEditModel);
            }

            bookToBeEdited.Title = bookEditModel.Title;
            bookToBeEdited.Description = bookEditModel.Description;

            try
            {
                _repository.EditBook(bookToBeEdited);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(_repository.GetBookById(id).Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _repository.GetBookById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var book = _repository.GetBookById(id);

            _repository.DeleteBook(book);

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return _repository.GetAllBooks().Any(e => e.Id == id);
        }
    }
}
