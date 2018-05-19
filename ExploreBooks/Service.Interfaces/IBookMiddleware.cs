﻿using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IBookMiddleware
    {
        Book GetBookInfoByDetails(string title, string description);
        Book CheckBook(string bookTitle);
        IReadOnlyList<Book> GetBooks(string bookTitles);
    }
}