using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Character> GetCharacterInfoByDetails(string name, string description)
        {
            return await _databaseService.Characters.SingleOrDefaultAsync(character => character.Name == name && character.Description == description);
        }

        public async Task<IReadOnlyList<Character>> GetAllCharacters()
        {
            return await _databaseService.Characters.ToListAsync();
        }

        public async Task<Character> GetCharacterById(Guid id)
        {
            return await _databaseService.Characters.SingleOrDefaultAsync(character => character.Id == id);
        }

        public async Task CreateCharacter(Character character)
        {
            _databaseService.Characters.Add(character);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditCharacter(Character character)
        {
            _databaseService.Characters.Update(character);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteCharacter(Character character)
        {
            _databaseService.Characters.Remove(character);

            await _databaseService.SaveChangesAsync();
        }
    }
}
