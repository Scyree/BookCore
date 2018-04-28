using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IAuthorGeneralUsage
    {
        Author GetAuthorInfoByDetails(string name, string description);
        IReadOnlyList<Author> GetAllAuthors();
        Author GetAuthorById(Guid id);
        void CreateAuthor(Author author);
        void EditAuthor(Author author);
        void DeleteAuthor(Author author);
        Author CheckAuthor(string description);
        List<Author> GetAuthors(string description);
    }
}
