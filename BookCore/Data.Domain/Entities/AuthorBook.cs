using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class AuthorBook
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid BookId { get; set; }

        public static AuthorBook CreateAuthorBook(Guid authorId, Guid bookId)
        {
            var instance = new AuthorBook
            {
                Id = Guid.NewGuid(),
                AuthorId = authorId,
                BookId = bookId
            };
            
            return instance;
        }
    }
}
