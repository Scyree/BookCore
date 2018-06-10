using System;
using System.Collections.Generic;
using Domain.Data;

namespace Business.Interfaces
{
    public interface IBooksForMoodService
    {
        IReadOnlyList<BooksForMood> GetAllBooksForMoods();
        void CreateBooksForMood(Guid userId, string title, string description, string books);
        BooksForMood GetBooksForMoodById(Guid id);
    }
}
