using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class BooksWithinBooksForMoodRepository : IBooksWithinBooksForMoodRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public BooksWithinBooksForMoodRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<BooksWithinBooksForMood> GetAllBooksWithinBooksForMood()
        {
            return _databaseService.BooksWithinBooksForMoods.ToList();
        }

        public BooksWithinBooksForMood GetBooksWithinBooksForMoodById(Guid id)
        {
            return _databaseService.BooksWithinBooksForMoods.SingleOrDefault(booksWithinBooksForMood => booksWithinBooksForMood.Id == id);
        }

        public void CreateBooksWithinBooksForMood(BooksWithinBooksForMood booksWithinBooksForMood)
        {
            _databaseService.BooksWithinBooksForMoods.Add(booksWithinBooksForMood);

            _databaseService.SaveChanges();
        }

        public void EditBooksWithinBooksForMood(BooksWithinBooksForMood booksWithinBooksForMood)
        {
            _databaseService.BooksWithinBooksForMoods.Update(booksWithinBooksForMood);

            _databaseService.SaveChanges();
        }

        public void DeleteBooksWithinBooksForMood(BooksWithinBooksForMood booksWithinBooksForMood)
        {
            _databaseService.BooksWithinBooksForMoods.Remove(booksWithinBooksForMood);

            _databaseService.SaveChanges();
        }
    }
}
