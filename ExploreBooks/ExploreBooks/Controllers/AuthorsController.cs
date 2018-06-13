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

        public IActionResult Index(string pageNumber)
        {
            var value = 0;

            if (pageNumber != null)
            {
                value = Int32.Parse(pageNumber);
            }

            return View(_service.GetFirstNAuthors(value));
        }

        [HttpGet, ActionName("details")]
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

        [HttpGet, ActionName("create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost, ActionName("create")]
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

        [HttpGet, ActionName("edit")]
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

        [HttpPost, ActionName("edit")]
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

        [HttpGet, ActionName("delete")]
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

        [HttpPost, ActionName("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _service.DeleteAuthor(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
