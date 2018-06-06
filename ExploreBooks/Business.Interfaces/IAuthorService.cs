using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Data;
using Microsoft.AspNetCore.Http;

namespace Business.Interfaces
{
    public interface IAuthorService
    {
        IReadOnlyList<Author> GetAllAuthors();
        IReadOnlyList<Book> GetBooksForSpecifiedAuthor(string givenAuthor);
        Task CreateAuthor(IFormFile image, string name, string description, string books);
        Task EditAuthor(Guid id, IFormFile image, string description, string books);
        void DeleteAuthor(Guid id);
        Author GetAuthorById(Guid id);
    }
}
