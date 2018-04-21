using System;
using System.Collections.Generic;
using Data.Domain.Entities;

namespace Data.Domain.Interfaces.Repositories
{
    public interface IGenreBookRepository
    {
        List<GenreBook> GetAllGenreBooksBasedOnBookId(Guid bookId);
        List<GenreBook> GetAllGenreBooksBasedOnGenreId(Guid genreId);
        GenreBook GetGenreBookById(Guid genreId, Guid bookId);
        void CreateGenreBook(GenreBook genreBook);
        void EditGenreBook(GenreBook genreBook);
        void DeleteGenreBook(GenreBook genreBook);
    }
}
