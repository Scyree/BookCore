using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class BooksForMoodRepository : IBooksForMoodRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public BooksForMoodRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IReadOnlyList<BooksForMood>> GetAllBooksForMoods()
        {
            return await _databaseService.BooksForMoods.ToListAsync();
        }

        public async Task<BooksForMood> GetBooksForMoodById(Guid id)
        {
            return await _databaseService.BooksForMoods.SingleOrDefaultAsync(booksForMood => booksForMood.Id == id);
        }

        public async Task CreateBooksForMood(BooksForMood booksForMood)
        {
            _databaseService.BooksForMoods.Add(booksForMood);

            await _databaseService.SaveChangesAsync();
        }

        public async Task EditBooksForMood(BooksForMood booksForMood)
        {
            _databaseService.BooksForMoods.Update(booksForMood);

            await _databaseService.SaveChangesAsync();
        }

        public async Task DeleteBooksForMood(BooksForMood booksForMood)
        {
            _databaseService.BooksForMoods.Remove(booksForMood);

            await _databaseService.SaveChangesAsync();
        }
    }
}
