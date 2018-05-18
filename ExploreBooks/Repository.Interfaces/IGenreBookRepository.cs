using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface IGenreBookRepository
    {
        Task<IReadOnlyList<GenreBook>> GetAllGenreBooksBasedOnBookId(Guid bookId);
        Task<IReadOnlyList<GenreBook>> GetAllGenreBooksBasedOnGenreId(Guid genreId);
        Task<GenreBook> GetGenreBookById(Guid genreId, Guid bookId);
        Task CreateGenreBook(GenreBook genreBook);
        Task EditGenreBook(GenreBook genreBook);
        Task DeleteGenreBook(GenreBook genreBook);
    }
}
