using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class GenreBook
    {
        [Key]
        public Guid Id { get; set; }
        public Guid GenreId { get; set; }
        public Guid BookId { get; set; }

        public static GenreBook CreateGenreBook(Guid genreId, Guid bookId)
        {
            var instance = new GenreBook
            {
                Id = Guid.NewGuid(),
                GenreId = genreId,
                BookId = bookId
            };

            return instance;
        }
    }
}
