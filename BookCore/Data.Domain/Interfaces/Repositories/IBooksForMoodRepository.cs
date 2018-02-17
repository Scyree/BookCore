using Data.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Data.Domain.Interfaces.Repositories
{
    public interface IBooksForMoodRepository
    {
        IReadOnlyList<BooksForMood> GetAllBooksForMoods();
        BooksForMood GetBooksForMoodById(Guid id);
        void CreateBooksForMood(BooksForMood booksForMood);
        void EditBooksForMood(BooksForMood booksForMood);
        void DeleteBooksForMood(BooksForMood booksForMood);
    }
}
