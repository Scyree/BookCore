using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class BooksForMood
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Likes { get; set; }

        public ICollection<Book> Books { get; set; }

        public ICollection<Post> Posts { get; set; }

        public static BooksForMood CreateBooksForMood(Guid userId, string title, string description)//, int likes, List<Book> books, List<Comment> comments)
        {
            var instance = new BooksForMood
            {
                Id = Guid.NewGuid(),
                Likes = 0,
                Books = new List<Book>(),
                Posts = new List<Post>()
            };

            instance.UpdateBooksForMood(userId, title, description);//, likes, books, comments);

            return instance;
        }

        private void UpdateBooksForMood(Guid userId, string title, string description)//, int likes, List<Book> books, List<Comment> comments)
        {
            UserId = userId;
            Title = title;
            Description = description;
            //Likes = likes;
            //Books = books;
            //Comments = comments;
        }
    }
}
