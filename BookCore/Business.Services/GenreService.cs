using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Domain.Interfaces.Services;

namespace Business.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;

        public GenreService(IGenreRepository repository)
        {
            _repository = repository;
        }

        public Genre CheckGenre(string description)
        {
            var check = _repository.GetAllGenres().SingleOrDefault(genre => genre.Text == description);

            if (check == null)
            {
                var genre = Genre.CreateGenre(description);
                _repository.CreateGenre(genre);

                return genre;
            }

            return check;
        }

        public List<Genre> GetGenres(string description)
        {
            var bruteGenre = description.Replace(" ", "");
            var genres = bruteGenre.Split(",");
            var genreList = new List<Genre>();

            foreach (var genre in genres)
            {
                genreList.Add(CheckGenre(genre));
            }

            return genreList;
        }

        public List<Genre> GetGenreBasedOnBookId(Guid bookId)
        {
            return _repository.GetAllGenres().Where(genre => genre.BooksId == bookId).ToList();
        }

    }
}
