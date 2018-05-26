using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interfaces;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Business.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;
        private readonly IGenreBookMiddleware _genreBookMiddleware;
        private readonly IBookRepository _bookRepository;

        public GenreService(IGenreRepository repository, IGenreBookMiddleware genreBookMiddleware, IBookRepository bookRepository)
        {
            _repository = repository;
            _genreBookMiddleware = genreBookMiddleware;
            _bookRepository = bookRepository;
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

        public IReadOnlyList<Genre> GetGenres(string description)
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

        public Genre GetGenreById(Guid id)
        {
            return _repository.GetGenreById(id);
        }

        public IReadOnlyList<Book> GetBooksForSpecifiedGenre(string text)
        {
            var searchedGenre = _repository.GetAllGenres().SingleOrDefault(genre => genre.Text == text);
            var books = new List<Book>();

            if (searchedGenre != null)
            {
                var genreBooks = _genreBookMiddleware.GetAllGenreBooksBasedOnGenreId(searchedGenre.Id);
                
                foreach (var genreBook in genreBooks)
                {
                    books.Add(_bookRepository.GetBookById(genreBook.BookId));
                }


                return books;
            }

            return null;
        }

        public int GetNumberOfBooksForSpecifiedGenre(string text)
        {
            var searchedGenre = _repository.GetAllGenres().SingleOrDefault(genre => genre.Text == text);
            var count = 0;

            if (searchedGenre != null)
            {
                count += _genreBookMiddleware.GetAllGenreBooksBasedOnGenreId(searchedGenre.Id).Count;
                
                return count;
            }

            return count;
        }

        public IReadOnlyList<Genre> GetAllGenres()
        {
            return _repository.GetAllGenres().OrderBy(genre => genre.Text).ToList();
        }
    }
}
