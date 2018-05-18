using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public AuthorRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Author> GetAuthorInfoByDetails(string name, string description)
        {
            return await _databaseService.Authors.SingleOrDefaultAsync(author => author.Name == name && author.Description == description);
        }

        public async Task<IReadOnlyList<Author>> GetAllAuthors()
        {
            return await _databaseService.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorById(Guid id)
        {
            return await _databaseService.Authors.SingleOrDefaultAsync(author => author.Id == id);
        }

        public async Task CreateAuthor(Author author)
        {
            _databaseService.Authors.Add(author);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditAuthor(Author author)
        {
            _databaseService.Authors.Update(author);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteAuthor(Author author)
        {
            _databaseService.Authors.Remove(author);

            await _databaseService.SaveChangesAsync();
        }
    }
}
