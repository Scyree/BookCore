using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IBooksWithinBooksForMoodRepository
    {
        IReadOnlyList<BooksWithinBooksForMood> GetAllBooksWithinBooksForMood();
        BooksWithinBooksForMood GetBooksWithinBooksForMoodById(Guid id);
        void CreateBooksWithinBooksForMood(BooksWithinBooksForMood booksWithinBooksForMood);
        void EditBooksWithinBooksForMood(BooksWithinBooksForMood booksWithinBooksForMood);
        void DeleteBooksWithinBooksForMood(BooksWithinBooksForMood booksWithinBooksForMood);
    }
}
