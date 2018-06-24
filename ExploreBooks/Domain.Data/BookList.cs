using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class BookList
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Likes { get; set; }

        public ICollection<BookListContent> Books { get; set; }

        public ICollection<Post> Posts { get; set; }

        public static BookList CreateBookList(Guid userId, string title, string description)
        {
            var instance = new BookList
            {
                Id = Guid.NewGuid(),
                Likes = 0,
                Books = new List<BookListContent>(),
                Posts = new List<Post>()
            };

            instance.UpdateBookList(userId, title, description);

            return instance;
        }

        private void UpdateBookList(Guid userId, string title, string description)
        {
            UserId = userId;
            Title = title;
            Description = description;
        }
    }
}
