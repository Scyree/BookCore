using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IReadOnlyList<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(Guid id);
        Task<Author> GetAuthorInfoByDetails(string name, string description);
        Task CreateAuthor(Author author);
        Task EditAuthor(Author author);
        Task DeleteAuthor(Author author);
    }
}
