using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
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
