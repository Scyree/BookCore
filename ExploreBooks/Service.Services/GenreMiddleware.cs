using System;
using System.Collections.Generic;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class GenreMiddleware : IGenreMiddleware
    {
        private readonly IGenreRepository _repository;
        private readonly IGenreBookMiddleware _genreBookMiddleware;
        private readonly IBookRepository _bookRepository;

        public GenreMiddleware(IGenreRepository repository, IGenreBookMiddleware genreBookMiddleware, IBookRepository bookRepository)
        {
            _repository = repository;
            _genreBookMiddleware = genreBookMiddleware;
            _bookRepository = bookRepository;
        }

        public Genre CheckGenre(string description)
        {
            var check = _repository.GetGenreBasedOnText(description);

            if (check == null)
            {
                var genre = Genre.CreateGenre(description);
                _repository.CreateGenre(genre);

                return genre;
            }

            return check;
        }

        public Genre GetGenreById(Guid genreId)
        {
            return _repository.GetGenreById(genreId);
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

        public List<Book> GetBooksForSpecifiedGenre(string text)
        {
            var searchedGenre = _repository.GetGenreBasedOnText(text);
            var books = new List<Book>();

            if (searchedGenre != null)
            {
                var genreBooks = _genreBookMiddleware.GetAllGenreBooksBasedOnGenreId(searchedGenre.Id);

                foreach (var genreBook in genreBooks)
                {
                    books.Add(_bookRepository.GetBookById(genreBook.BookId));
                }
            }

            return books;
        }

        public int GetNumberOfBooksForSpecifiedGenre(string text)
        {
            return GetBooksForSpecifiedGenre(text).Count;
        }

        public List<Genre> GetAllGenres()
        {
            return _repository.GetAllGenres();
        }

        public string GetRandomGenre()
        {
            var random = new Random();
            var genresList = new List<string>
            {
                "SF",
                "Tragedy",
                "Fantasy",
                "Mythology",
                "Adventure",
                "Mystery",
                "Romance",
                "Action",
                "Thriller",
                "Adventure"
            };

            var index = random.Next(0, genresList.Count - 1);

            return genresList[index];
        }
    }
}
