using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Business.Interfaces
{
    public interface IBookService
    {
        List<Book> SearchBooks(string text);
        List<Book> GetAllBooks();
        List<Book> GetFirstNBooks(int count);
        Task CreateBook(IFormFile image, string title, string description, string details, string authors, string genres);
        Task EditBook(Guid id, IFormFile image, string description, string details);
        void DeleteBook(Guid id);
        Book GetBookById(Guid id);
        List<SelectListItem> GetAllBooksForRecommendation(Guid bookId);
    }
}
