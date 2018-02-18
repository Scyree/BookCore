using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Presentation.Models.BookViewModels;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Data.Domain.Interfaces.Services;

namespace Presentation.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _service;
        private readonly IBookRepository _repository;
        private readonly IHostingEnvironment _env;

        public BooksController(IBookService service, IBookRepository repository, IHostingEnvironment env)
        {
            _service = service;
            _repository = repository;
            _env = env;
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
        public async Task<IActionResult> Create([Bind("AuthorId, Title, Description, Details, Image")] BookCreateModel bookCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookCreateModel);
            }

            var value = Guid.NewGuid();
            var path = Path.Combine(_env.WebRootPath, "Books/" + value);

            if (bookCreateModel.Image.Length > 0)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(Path.Combine(path, bookCreateModel.Image.FileName), FileMode.Create))
                {
                    await bookCreateModel.Image.CopyToAsync(fileStream);
                }
            }

            var absolutePath = "~/Books/" + value;

            _repository.CreateBook(
                Book.CreateBook(
                    bookCreateModel.Title,
                    bookCreateModel.Description,
                    absolutePath,
                    bookCreateModel.Image.FileName,
                    bookCreateModel.AuthorId,
                    bookCreateModel.Details
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
                book.AuthorId,
                book.Title,
                book.Description,
                book.Details
            );

            return View(bookEditModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId, Title, Description, Details, Image")] BookEditModel bookEditModel)
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

            bookToBeEdited.AuthorId = bookEditModel.AuthorId;
            bookToBeEdited.Details = bookEditModel.Details;
            bookToBeEdited.Title = bookEditModel.Title;
            bookToBeEdited.Description = bookEditModel.Description;

            try
            {
                if (bookEditModel.Image != null)
                {
                    var searchedPath = bookToBeEdited.Folder.Replace("~", "");
                    var path = _env.WebRootPath + searchedPath;
                    
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    
                    using (var fileStream = new FileStream(Path.Combine(path, bookToBeEdited.ImageName), FileMode.Create))
                    {
                        await bookEditModel.Image.CopyToAsync(fileStream);
                    }
                }
                
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
            _service.DeleteFileForGivenId(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return _repository.GetAllBooks().Any(e => e.Id == id);
        }
    }
}
