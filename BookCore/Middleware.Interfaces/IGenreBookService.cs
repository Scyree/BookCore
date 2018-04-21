using System;
using System.Collections.Generic;
using Data.Domain.Entities;

namespace Middleware.Interfaces
{
    public interface IGenreBookService
    {
        void CheckGenreBook(Guid genreId, Guid bookId);
        List<GenreBook> GetAllGenreBooksBasedOnBookId(Guid bookId);
        List<GenreBook> GetAllGenreBooksBasedOnGenreId(Guid genreId);
        void DeleteForGenreId(Guid genreId);
        void DeleteForBookId(Guid bookId);
    }
}
