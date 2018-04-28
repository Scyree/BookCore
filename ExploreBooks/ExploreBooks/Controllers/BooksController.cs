using System;
using System.Threading.Tasks;
using Business.Interfaces;
using ExploreBooks.Models.BookViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(_service.GetAllBooks());
        }

        // GET: Books/Details
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _service.GetBookById(id.Value);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Authors, Genres, Title, Description, Details, Image")] BookCreateModel bookCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookCreateModel);
            }

            await _service.CreateBook(bookCreateModel.Image, bookCreateModel.Title, bookCreateModel.Description, bookCreateModel.Details, bookCreateModel.Authors, bookCreateModel.Genres);

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Edit
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _service.GetBookById(id.Value);

            if (book == null)
            {
                return NotFound();
            }


            var bookEditModel = new BookEditModel(
                book.Description,
                book.Details
            );

            return View(bookEditModel);
        }

        // POST: Books/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Authors, Genres, Description, Details, Image")] BookEditModel bookEditModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookEditModel);
            }

            await _service.EditBook(id, bookEditModel.Image, bookEditModel.Description, bookEditModel.Details, bookEditModel.Genres, bookEditModel.Authors);

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Delete
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _service.GetBookById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteBook(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
