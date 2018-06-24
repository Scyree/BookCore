using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IAuthorRepository
    {
        List<Author> GetAllAuthors();
        List<Author> GetFirstNAuthors(int skipNumber, int takeNumber);
        Author GetAuthorBasedOnName(string name);
        Author GetAuthorById(Guid id);
        Author GetAuthorInfoByDetails(string name, string description);
        void CreateAuthor(Author author);
        void EditAuthor(Author author);
        void DeleteAuthor(Author author);
    }
}
