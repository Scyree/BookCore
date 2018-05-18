using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class GenreBookRepository : IGenreBookRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public GenreBookRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IReadOnlyList<GenreBook>> GetAllGenreBooksBasedOnBookId(Guid bookId)
        {
            return await _databaseService.GenreBooks.Where(genre => genre.BookId == bookId).ToListAsync();
        }

        public async Task<IReadOnlyList<GenreBook>> GetAllGenreBooksBasedOnGenreId(Guid genreId)
        {
            return await _databaseService.GenreBooks.Where(book => book.GenreId == genreId).ToListAsync();
        }

        public async Task<GenreBook> GetGenreBookById(Guid genreId, Guid bookId)
        {
            return await _databaseService.GenreBooks.SingleOrDefaultAsync(genreBook => genreBook.GenreId == genreId && genreBook.BookId == bookId);
        }

        public async Task CreateGenreBook(GenreBook genreBook)
        {
            _databaseService.GenreBooks.Add(genreBook);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditGenreBook(GenreBook genreBook)
        {
            _databaseService.GenreBooks.Update(genreBook);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteGenreBook(GenreBook genreBook)
        {
            _databaseService.GenreBooks.Remove(genreBook);

            await _databaseService.SaveChangesAsync();
        }
    }
}
