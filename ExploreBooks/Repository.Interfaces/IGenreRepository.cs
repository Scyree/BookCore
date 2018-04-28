using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
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
