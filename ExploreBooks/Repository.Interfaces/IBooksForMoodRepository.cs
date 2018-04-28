using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
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
