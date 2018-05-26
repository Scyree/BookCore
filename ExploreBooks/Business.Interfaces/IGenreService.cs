using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IGenreService
    {
        Genre CheckGenre(string description);
        IReadOnlyList<Genre> GetGenres(string description);
        Genre GetGenreById(Guid id);
        IReadOnlyList<Book> GetBooksForSpecifiedGenre(string text);
        int GetNumberOfBooksForSpecifiedGenre(string text);
        IReadOnlyList<Genre> GetAllGenres();
    }
}
