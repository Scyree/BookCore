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

        public List<Like> Likes { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public static Review CreateReview(double bookRating, string description, Guid userId, Guid bookId)//, DateTime date, List<Comment> comments)
        {
            var instance = new Review
            {
                Id = Guid.NewGuid(),
                Likes = new List<Like>(),
                Date = DateTime.UtcNow,
                Comments = new List<Comment>()
            };

            instance.UpdateReview(bookRating, description, userId, bookId);//, date, comments);

            return instance;
        }

        private void UpdateReview(double bookRating, string description, Guid userId, Guid bookId)//, DateTime date, List<Comment> comments)
        {
            BookRating = bookRating;
            Description = description;
            UserId = userId;
            BookId = bookId;
            //Date = date;
            //Comments = comments;
        }
    }
}
