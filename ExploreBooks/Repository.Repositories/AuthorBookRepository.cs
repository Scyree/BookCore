using Repository.Interfaces;
using Domain.Data;
using Domain.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class AuthorBookRepository : IAuthorBookRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public AuthorBookRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IReadOnlyList<AuthorBook>> GetAllAuthorBooksBasedOnBookId(Guid bookId)
        {
            return await _databaseService.AuthorBooks.Where(author => author.BookId == bookId).ToListAsync();
        }

        public async Task<IReadOnlyList<AuthorBook>> GetAllAuthorBooksBasedOnAuthorId(Guid authorId)
        {
            return await _databaseService.AuthorBooks.Where(book => book.AuthorId == authorId).ToListAsync();
        }

        public async Task<AuthorBook> GetAuthorBookById(Guid authorId, Guid bookId)
        {
            return await _databaseService.AuthorBooks.SingleOrDefaultAsync(authorBook => authorBook.AuthorId == authorId && authorBook.BookId == bookId);
        }

        public async Task CreateAuthorBook(AuthorBook authorBook)
        {
            _databaseService.AuthorBooks.Add(authorBook);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditAuthorBook(AuthorBook authorBook)
        {
            _databaseService.AuthorBooks.Update(authorBook);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteAuthorBook(AuthorBook authorBook)
        {
            _databaseService.AuthorBooks.Remove(authorBook);

            await _databaseService.SaveChangesAsync();
        }
    }
}
