using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public double Rate { get; set; }

        public static Rating CreateRating(Guid userId, Guid bookId)
        {
            var instance = new Rating
            {
                Id = Guid.NewGuid(),
                Rate = 0.0
            };

            instance.UpdateRating(userId, bookId);

            return instance;
        }

        private void UpdateRating(Guid userId, Guid bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}
