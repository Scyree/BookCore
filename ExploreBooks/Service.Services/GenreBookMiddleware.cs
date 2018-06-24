using System;
using System.Collections.Generic;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class GenreBookMiddleware : IGenreBookMiddleware
    {
        private readonly IGenreBookRepository _repository;

        public GenreBookMiddleware(IGenreBookRepository repository)
        {
            _repository = repository;
        }

        public void CheckGenreBook(Guid genreId, Guid bookId)
        {
            var check = _repository.GetGenreBookById(genreId, bookId);

            if (check == null)
            {
                var genreBook = GenreBook.CreateGenreBook(genreId, bookId);
                _repository.CreateGenreBook(genreBook);
            }
        }

        public List<GenreBook> GetAllGenreBooksBasedOnBookId(Guid bookId)
        {
            return _repository.GetAllGenreBooksBasedOnBookId(bookId);
        }

        public List<GenreBook> GetAllGenreBooksBasedOnGenreId(Guid genreId)
        {
            return _repository.GetAllGenreBooksBasedOnGenreId(genreId);
        }

        public void DeleteForGenreId(Guid genreId)
        {
            var genreBooks = _repository.GetAllGenreBooksBasedOnGenreId(genreId);

            foreach (var genreBook in genreBooks)
            {
                _repository.DeleteGenreBook(genreBook);
            }
        }

        public void DeleteForBookId(Guid bookId)
        {
            var genreBooks = _repository.GetAllGenreBooksBasedOnBookId(bookId);

            foreach (var genreBook in genreBooks)
            {
                _repository.DeleteGenreBook(genreBook);
            }
        }
    }
}
