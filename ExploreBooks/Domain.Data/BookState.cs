using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class BookState
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TargetId { get; set; }

        public int State { get; set; }

        public bool IsFavorite { get; set; }

        public DateTime DateModified { get; set; }

        public static BookState CreateBookState(Guid userId, Guid targetId)
        {
            var instance = new BookState
            {
                Id = Guid.NewGuid(),
                State = 0,
                IsFavorite = false,
                DateModified = DateTime.UtcNow
            };

            instance.UpdateBookState(userId, targetId);

            return instance;
        }

        private void UpdateBookState(Guid userId, Guid targetId)
        {
            UserId = userId;
            TargetId = targetId;
        }
    }
}
