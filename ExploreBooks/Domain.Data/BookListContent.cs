using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class BookListContent
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BookListId { get; set; }

        public Guid BookId { get; set; }

        public static BookListContent Create(Guid bookListId, Guid bookId)
        {
            var instance = new BookListContent
            {
                Id = Guid.NewGuid()
            };

            instance.Update(bookListId, bookId);

            return instance;
        }

        private void Update(Guid bookListId, Guid bookId)
        {
            BookListId = bookListId;
            BookId = bookId;
        }
    }
}
