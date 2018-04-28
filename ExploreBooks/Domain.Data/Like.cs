using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Like
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TargetId { get; set; }

        public bool Positivity { get; set; }

        public static Like CreateLike(Guid userId, Guid targetId, bool positivity)
        {
            var instance = new Like
            {
                Id = Guid.NewGuid()
            };

            instance.UpdateLike(userId, targetId, positivity);

            return instance;
        }

        private void UpdateLike(Guid userId, Guid targetId, bool positivity)
        {
            UserId = userId;
            TargetId = targetId;
            Positivity = positivity;
        }
    }
}
