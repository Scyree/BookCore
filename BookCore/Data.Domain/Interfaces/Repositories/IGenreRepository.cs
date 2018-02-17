using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        IReadOnlyList<Genre> GetAllGenres();
        Genre GetGenreById(Guid id);
        void CreateGenre(Genre genre);
        void EditGenre(Genre genre);
        void DeleteGenre(Genre genre);
    }
}
