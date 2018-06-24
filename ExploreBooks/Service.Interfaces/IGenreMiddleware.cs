using System;
using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface IGenreMiddleware
    {
        List<Genre> GetGenres(string description);
        List<Book> GetBooksForSpecifiedGenre(string text);
        List<Genre> GetAllGenres();
        Genre GetGenreById(Guid genreId);
        Genre CheckGenre(string description);
        int GetNumberOfBooksForSpecifiedGenre(string text);
        string GetRandomGenre();
    }
}
