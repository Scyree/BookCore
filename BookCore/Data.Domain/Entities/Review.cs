using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public double BookRating { get; set; }

        public int Likes { get; set; }

        public DateTime Date { get; set; }

        public List<Comment> Comments { get; set; }

        public static Review CreateReview(string description, Guid userId, Guid bookId, DateTime date, List<Comment> comments)
        {
            var instance = new Review
            {
                Id = Guid.NewGuid(),
                BookRating = 0.0,
                Likes = 0
            };

            instance.UpdateReview(description, userId, bookId, date, comments);

            return instance;
        }

        private void UpdateReview(string description, Guid userId, Guid bookId, DateTime date, List<Comment> comments)
        {
            Description = description;
            UserId = userId;
            BookId = bookId;
            Date = date;
            Comments = comments;
        }
    }
}
