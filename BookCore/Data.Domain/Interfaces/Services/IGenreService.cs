using System;
using System.Collections.Generic;
using Data.Domain.Entities;

namespace Data.Domain.Interfaces.Services
{
    public interface IGenreService
    {
        Genre CheckGenre(string description);
        List<Genre> GetGenres(string description);
        Genre GetGenreById(Guid id);
    }
}
