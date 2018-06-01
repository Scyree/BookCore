using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class FollowUser
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid FollowId { get; set; }
        
        public static FollowUser CreateFollowUser(Guid userId, Guid followId)
        {
            var instance = new FollowUser
            {
                Id = Guid.NewGuid()
            };

            instance.UpdateFollowUser(userId, followId);

            return instance;
        }

        private void UpdateFollowUser(Guid userId, Guid followId)
        {
            UserId = userId;
            FollowId = followId;
        }
    }
}
