using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Presentation.Models.CharacterViewModels;

namespace Presentation.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ICharacterRepository _repository;

        public CharactersController(ICharacterRepository repository)
        {
            _repository = repository;
        }

        // GET: Characters
        public IActionResult Index()
        {
            return View(_repository.GetAllCharacters());
        }

        // GET: Characters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = _repository.GetCharacterById(id.Value);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name, Description")] CharacterCreateModel characterCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(characterCreateModel);
            }

            _repository.CreateCharacter(
                Character.CreateCharacter(
                    characterCreateModel.Name,
                    characterCreateModel.Description
                )
            );

            return RedirectToAction(nameof(Index));
        }

        // GET: Characters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = _repository.GetCharacterById(id.Value);
            if (character == null)
            {
                return NotFound();
            }

            var characterEditModel = new CharacterEditModel(
                character.Name,
                character.Description
            );

            return View(characterEditModel);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name, Description")] CharacterEditModel characterEditModel)
        {
            var characterToBeEdited = _repository.GetCharacterById(id);

            if (characterToBeEdited == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(characterEditModel);
            }

            characterToBeEdited.Name = characterEditModel.Name;
            characterToBeEdited.Description = characterEditModel.Description;

            try
            {
                _repository.EditCharacter(characterToBeEdited);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(_repository.GetCharacterById(id).Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Characters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = _repository.GetCharacterById(id.Value);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var character = _repository.GetCharacterById(id);

            _repository.DeleteCharacter(character);

            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(Guid id)
        {
            return _repository.GetAllCharacters().Any(e => e.Id == id);
        }
    }
}
