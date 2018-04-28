using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
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

        public IReadOnlyList<BooksForMood> GetAllBooksForMoods()
        {
            return _databaseService.BooksForMoods.ToList();
        }

        public BooksForMood GetBooksForMoodById(Guid id)
        {
            return _databaseService.BooksForMoods.SingleOrDefault(booksForMood => booksForMood.Id == id);
        }

        public void CreateBooksForMood(BooksForMood booksForMood)
        {
            _databaseService.BooksForMoods.Add(booksForMood);

            _databaseService.SaveChanges();
        }

        public void EditBooksForMood(BooksForMood booksForMood)
        {
            _databaseService.BooksForMoods.Update(booksForMood);

            _databaseService.SaveChanges();
        }

        public void DeleteBooksForMood(BooksForMood booksForMood)
        {
            _databaseService.BooksForMoods.Remove(booksForMood);

            _databaseService.SaveChanges();
        }
    }
}
