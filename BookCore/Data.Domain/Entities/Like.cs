using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Domain.Entities
{
    public class Like
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TargetId { get; set; }

        public static Like CreateLike(Guid userId, Guid targetId)
        {
            var instance = new Like
            {
                Id = Guid.NewGuid()
            };

            instance.UpdateLike(userId, targetId);

            return instance;
        }

        private void UpdateLike(Guid userId, Guid targetId)
        {
            UserId = userId;
            TargetId = targetId;
        }
    }
}
