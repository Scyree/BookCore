using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class BooksWithinBooksForMood
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BooksForMoodId { get; set; }

        public Guid BookId { get; set; }

        public static BooksWithinBooksForMood Create(Guid booksForMoodId, Guid bookId)
        {
            var instance = new BooksWithinBooksForMood
            {
                Id = Guid.NewGuid()
            };

            instance.Update(booksForMoodId, bookId);

            return instance;
        }

        private void Update(Guid booksForMoodId, Guid bookId)
        {
            BooksForMoodId = booksForMoodId;
            BookId = bookId;
        }
    }
}
