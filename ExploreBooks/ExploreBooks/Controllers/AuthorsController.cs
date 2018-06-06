using System;
using System.Threading.Tasks;
using Business.Interfaces;
using ExploreBooks.Models.AuthorViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExploreBooks.Controllers
{
    [Route("authors/[action]")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        // GET: Authors
        public IActionResult Index()
        {
            return View(_service.GetAllAuthors());
        }

        // GET: Authors/Details
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _service.GetAuthorById(id.Value);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Description, Books, Image")] AuthorCreateModel authorCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(authorCreateModel);
            }

            await _service.CreateAuthor(authorCreateModel.Image, authorCreateModel.Name, authorCreateModel.Description, authorCreateModel.Books);

            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Edit
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _service.GetAuthorById(id.Value);

            if (author == null)
            {
                return NotFound();
            }

            var authorEditModel = new AuthorEditModel(
                author.Description
            );

            return View(authorEditModel);
        }

        // POST: Authors/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Description, Books, Image")] AuthorEditModel authorEditModel)
        {
            if (!ModelState.IsValid)
            {
                return View(authorEditModel);
            }

            await _service.EditAuthor(id, authorEditModel.Image, authorEditModel.Description, authorEditModel.Books);

            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Delete
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _service.GetAuthorById(id.Value);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteAuthor(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
