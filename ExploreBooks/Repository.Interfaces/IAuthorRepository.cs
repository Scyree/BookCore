using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
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
