using System;
using System.Collections.Generic;
using System.Linq;
using Data.Domain.Entities;
using Data.Domain.Interfaces.Repositories;
using Data.Persistence;

namespace Business.Repositories
{
    public class GenreBookRepository : IGenreBookRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public GenreBookRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public List<GenreBook> GetAllGenreBooksBasedOnBookId(Guid bookId)
        {
            return _databaseService.GenreBooks.Where(genre => genre.BookId == bookId).ToList();
        }

        public List<GenreBook> GetAllGenreBooksBasedOnGenreId(Guid genreId)
        {
            return _databaseService.GenreBooks.Where(book => book.GenreId == genreId).ToList();
        }

        public GenreBook GetGenreBookById(Guid genreId, Guid bookId)
        {
            return _databaseService.GenreBooks.SingleOrDefault(genreBook => genreBook.GenreId == genreId && genreBook.BookId == bookId);
        }

        public void CreateGenreBook(GenreBook genreBook)
        {
            _databaseService.GenreBooks.Add(genreBook);

            _databaseService.SaveChanges();
        }

        public void EditGenreBook(GenreBook genreBook)
        {
            _databaseService.GenreBooks.Update(genreBook);

            _databaseService.SaveChanges();
        }

        public void DeleteGenreBook(GenreBook genreBook)
        {
            _databaseService.GenreBooks.Remove(genreBook);

            _databaseService.SaveChanges();
        }
    }
}
