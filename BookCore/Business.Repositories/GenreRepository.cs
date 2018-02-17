using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Persistence;

namespace Business.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public GenreRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Genre> GetAllGenres()
        {
            return _databaseService.Genres.ToList();
        }

        public Genre GetGenreById(Guid id)
        {
            return _databaseService.Genres.SingleOrDefault(genre => genre.Id == id);
        }

        public void CreateGenre(Genre genre)
        {
            _databaseService.Genres.Add(genre);

            _databaseService.SaveChanges();
        }

        public void EditGenre(Genre genre)
        {
            _databaseService.Genres.Update(genre);

            _databaseService.SaveChanges();
        }

        public void DeleteGenre(Genre genre)
        {
            _databaseService.Genres.Remove(genre);

            _databaseService.SaveChanges();
        }
    }
}
