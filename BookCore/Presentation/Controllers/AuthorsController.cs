using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Presentation.Models.AuthorViewModels;

namespace Presentation.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorRepository _repository;

        public AuthorsController(IAuthorRepository repository)
        {
            _repository = repository;
        }

        // GET: Authors
        public IActionResult Index()
        {
            return View(_repository.GetAllAuthors());
        }

        // GET: Authors/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _repository.GetAuthorById(id.Value);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name, Description")] AuthorCreateModel authorCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(authorCreateModel);
            }

            _repository.CreateAuthor(
                Author.CreateAuthor(
                    authorCreateModel.Name,
                    authorCreateModel.Description
                )
            );

            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _repository.GetAuthorById(id.Value);
            if (author == null)
            {
                return NotFound();
            }

            var authorEditModel = new AuthorEditModel(
                author.Name,
                author.Description
            );

            return View(authorEditModel);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name, Description")] AuthorEditModel authorEditModel)
        {
            var authorToBeEdited = _repository.GetAuthorById(id);

            if (authorToBeEdited == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(authorEditModel);
            }

            authorToBeEdited.Name = authorEditModel.Name;
            authorToBeEdited.Description = authorEditModel.Description;

            try
            {
                _repository.EditAuthor(authorToBeEdited);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(_repository.GetAuthorById(id).Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _repository.GetAuthorById(id.Value);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var author = _repository.GetAuthorById(id);

            _repository.DeleteAuthor(author);

            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(Guid id)
        {
            return _repository.GetAllAuthors().Any(e => e.Id == id);
        }
    }
}
