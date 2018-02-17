using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class BooksForMood
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Likes { get; set; }

        public List<Book> Books { get; set; }

        public List<Comment> Comments { get; set; }

        public static BooksForMood CreateBooksForMood(Guid userId, string title, string description)//, int likes, List<Book> books, List<Comment> comments)
        {
            var instance = new BooksForMood
            {
                Id = Guid.NewGuid(),
                Likes = 0,
                Books = new List<Book>(),
                Comments = new List<Comment>()
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
