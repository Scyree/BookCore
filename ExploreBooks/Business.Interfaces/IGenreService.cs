using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IGenreService
    {
        Genre CheckGenre(string description);
        List<Genre> GetGenres(string description);
        Genre GetGenreById(Guid id);
    }
}
