using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IBooksForMoodRepository
    {
        Task<IReadOnlyList<BooksForMood>> GetAllBooksForMoods();
        Task<BooksForMood> GetBooksForMoodById(Guid id);
        Task CreateBooksForMood(BooksForMood booksForMood);
        Task EditBooksForMood(BooksForMood booksForMood);
        Task DeleteBooksForMood(BooksForMood booksForMood);
    }
}
