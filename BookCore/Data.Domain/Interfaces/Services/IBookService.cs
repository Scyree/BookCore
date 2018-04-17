using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Data.Domain.Interfaces.Services
{
    public interface IBookService
    {
        IReadOnlyList<Book> GetAllBooks();
        Task CreateBook(IFormFile image, string title, string description, string details, string authors, string genres);
        Task EditBook(Guid id, IFormFile image, string description, string details);
        void DeleteBook(Guid id);
        Book GetBookById(Guid id);
    }
}
