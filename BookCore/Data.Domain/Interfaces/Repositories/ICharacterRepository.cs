using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
{
    public interface ICharacterRepository
    {
        IReadOnlyList<Character> GetAllCharacters();
        Character GetCharacterById(Guid id);
        Character GetCharacterInfoByDetails(string name, string description);
        void CreateCharacter(Character character);
        void EditCharacter(Character character);
        void DeleteCharacter(Character character);
    }
}
