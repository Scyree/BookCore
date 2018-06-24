using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IGenreBookMiddleware
    {
        List<GenreBook> GetAllGenreBooksBasedOnBookId(Guid bookId);
        List<GenreBook> GetAllGenreBooksBasedOnGenreId(Guid genreId);
        void CheckGenreBook(Guid genreId, Guid bookId);
        void DeleteForGenreId(Guid genreId);
        void DeleteForBookId(Guid bookId);
    }
}
