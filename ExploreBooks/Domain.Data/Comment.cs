using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TargetId { get; set; }

        public string Text { get; set; }

        public ICollection<Like> Likes { get; set; }

        public DateTime Date { get; set; }

        public static Comment CreateComment(Guid userId, Guid targetId, string text)
        {
            var instance = new Comment
            {
                Id = Guid.NewGuid(),
                Likes = new List<Like>(),
                Date = DateTime.UtcNow
            };

            instance.UpdateComment(userId, targetId, text);

            return instance;
        }

        private void UpdateComment(Guid userId, Guid targetId, string text)
        {
            UserId = userId;
            TargetId = targetId;
            Text = text;
        }
    }
}
