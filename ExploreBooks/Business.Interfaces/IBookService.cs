﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Data;
using Microsoft.AspNetCore.Http;

namespace Business.Interfaces
{
    public interface IBookService
    {
        IReadOnlyList<Book> GetAllBooks();
        Task CreateBook(IFormFile image, string title, string description, string details, string authors, string genres);
        Task EditBook(Guid id, IFormFile image, string description, string details, string genres, string authors);
        void DeleteBook(Guid id);
        Book GetBookById(Guid id);
    }
}