using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public GenreRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IReadOnlyList<Genre>> GetAllGenres()
        {
            return await _databaseService.Genres.ToListAsync();
        }

        public async Task<Genre> GetGenreById(Guid id)
        {
            return await _databaseService.Genres.SingleOrDefaultAsync(genre => genre.Id == id);
        }

        public async Task CreateGenre(Genre genre)
        {
            _databaseService.Genres.Add(genre);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditGenre(Genre genre)
        {
            _databaseService.Genres.Update(genre);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteGenre(Genre genre)
        {
            _databaseService.Genres.Remove(genre);

            await _databaseService.SaveChangesAsync();
        }
    }
}
