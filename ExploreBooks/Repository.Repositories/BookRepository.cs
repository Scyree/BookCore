using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public BookRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Book> GetBookInfoByDetails(string title, string description)
        {
            return await _databaseService.Books.SingleOrDefaultAsync(book => book.Title == title && book.Description == description);
        }

        public async Task<IReadOnlyList<Book>> GetAllBooks()
        {
            return await _databaseService.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(Guid id)
        {
            return await _databaseService.Books.SingleOrDefaultAsync(book => book.Id == id);
        }

        public async Task CreateBook(Book book)
        {
            _databaseService.Books.Add(book);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditBook(Book book)
        {
            _databaseService.Books.Update(book);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteBook(Book book)
        {
            _databaseService.Books.Remove(book);

            await _databaseService.SaveChangesAsync();
        }
    }
}
