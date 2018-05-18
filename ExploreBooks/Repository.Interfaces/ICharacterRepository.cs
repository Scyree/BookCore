using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface ICharacterRepository
    {
        Task<IReadOnlyList<Character>> GetAllCharacters();
        Task<Character> GetCharacterById(Guid id);
        Task<Character> GetCharacterInfoByDetails(string name, string description);
        Task CreateCharacter(Character character);
        Task EditCharacter(Character character);
        Task DeleteCharacter(Character character);
    }
}
