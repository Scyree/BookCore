using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }

        public Guid TargetId { get; set; }

        public ICollection<Like> Likes { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public static Post CreatePost(string description, Guid userId, Guid targetId)
        {
            var instance = new Post
            {
                Id = Guid.NewGuid(),
                Likes = new List<Like>(),
                Date = DateTime.UtcNow,
                Comments = new List<Comment>()
            };

            instance.UpdatePost(description, userId, targetId);

            return instance;
        }

        private void UpdatePost(string description, Guid userId, Guid targetId)
        {
            Description = description;
            UserId = userId;
            TargetId = targetId;
        }
    }
}
