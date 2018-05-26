using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Recommendation
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public Guid UserId { get; set; }

        public Guid BookRecommended { get; set; }

        public string Reason { get; set; }

        public static Recommendation CreateRecommendation(Guid bookId, Guid userId, Guid bookRecommended, string reason)
        {
            var instance = new Recommendation
            {
                Id = Guid.NewGuid()
            };

            instance.UpdateRecommendation(bookId, userId, bookRecommended, reason);

            return instance;
        }

        private void UpdateRecommendation(Guid bookId, Guid userId, Guid bookRecommended, string reason)
        {
            BookId = bookId;
            UserId = userId;
            BookRecommended = bookRecommended;
            Reason = reason;
        }
    }
}
