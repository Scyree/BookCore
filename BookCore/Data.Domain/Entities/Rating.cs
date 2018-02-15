using System;

namespace Data.Domain.Entities
{
    public class Rating
    {
        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public double Rate { get; set; }

        public static Rating CreateRating(Guid userId, Guid bookId)
        {
            var instance = new Rating
            {
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
