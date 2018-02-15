using System;

namespace Data.Domain.Entities
{
    public class Recommandation
    {
        public Guid BookId { get; set; }

        public Guid UserId { get; set; }

        public Guid BookRecommanded { get; set; }

        public static Recommandation CreateRecommandation(Guid bookId, Guid userId, Guid bookRecommanded)
        {
            var instance = new Recommandation();

            instance.UpdateRecommandation(bookId, userId, bookRecommanded);

            return instance;
        }

        private void UpdateRecommandation(Guid bookId, Guid userId, Guid bookRecommanded)
        {
            BookId = bookId;
            UserId = userId;
            BookRecommanded = bookRecommanded;
        }
    }
}
