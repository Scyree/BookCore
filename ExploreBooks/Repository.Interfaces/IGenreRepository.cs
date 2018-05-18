using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IGenreRepository
    {
        Task<IReadOnlyList<Genre>> GetAllGenres();
        Task<Genre> GetGenreById(Guid id);
        Task CreateGenre(Genre genre);
        Task EditGenre(Genre genre);
        Task DeleteGenre(Genre genre);
    }
}
