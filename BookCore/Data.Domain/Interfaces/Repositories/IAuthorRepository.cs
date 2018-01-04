using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        IReadOnlyList<Author> GetAllAuthors();
        Author GetAuthorById(Guid id);
        Author GetAuthorInfoByDetails(string name, string description);
        void CreateAuthor(Author author);
        void EditAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}
