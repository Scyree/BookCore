using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public CharacterRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public Character GetCharacterInfoByDetails(string name, string description)
        {
            return _databaseService.Characters.SingleOrDefault(character => character.Name == name && character.Description == description);
        }

        public IReadOnlyList<Character> GetAllCharacters()
        {
            return _databaseService.Characters.ToList();
        }

        public Character GetCharacterById(Guid id)
        {
            return _databaseService.Characters.SingleOrDefault(character => character.Id == id);
        }

        public void CreateCharacter(Character character)
        {
            _databaseService.Characters.Add(character);

            _databaseService.SaveChanges();
        }

        public void EditCharacter(Character character)
        {
            _databaseService.Characters.Update(character);

            _databaseService.SaveChanges();
        }

        public void DeleteCharacter(Character character)
        {
            _databaseService.Characters.Remove(character);

            _databaseService.SaveChanges();
        }
    }
}
