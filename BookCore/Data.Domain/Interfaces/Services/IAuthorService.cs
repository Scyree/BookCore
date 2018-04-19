using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Data.Domain.Interfaces.Services
{
    public interface IAuthorService
    {
        IReadOnlyList<Author> GetAllAuthors();
        Author CheckAuthor(string description);
        List<Author> GetAuthors(string description);
        Task CreateAuthor(IFormFile image, string name, string description);
        Task EditAuthor(Guid id, IFormFile image, string name, string description);
        void DeleteAuthor(Guid id);
        Author GetAuthorById(Guid id);
    }
}
