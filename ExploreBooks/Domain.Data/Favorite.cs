using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Favorite
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TargetId { get; set; }

        public static Favorite CreateFavorite(Guid userId, Guid targetId)
        {
            var instance = new Favorite
            {
                Id = Guid.NewGuid()
            };

            instance.UpdateFavorite(userId, targetId);

            return instance;
        }

        private void UpdateFavorite(Guid userId, Guid targetId)
        {
            UserId = userId;
            TargetId = targetId;
        }
    }
}
