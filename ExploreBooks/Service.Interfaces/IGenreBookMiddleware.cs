using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IGenreBookMiddleware
    {
        void CheckGenreBook(Guid genreId, Guid bookId);
        IReadOnlyList<GenreBook> GetAllGenreBooksBasedOnBookId(Guid bookId);
        IReadOnlyList<GenreBook> GetAllGenreBooksBasedOnGenreId(Guid genreId);
        void DeleteForGenreId(Guid genreId);
        void DeleteForBookId(Guid bookId);
    }
}
