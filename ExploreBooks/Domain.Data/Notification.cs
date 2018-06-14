using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public static Notification CreateNotification(Guid userId, string content)
        {
            var instance = new Notification
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow
            };

            instance.UpdateNotification(userId, content);

            return instance;
        }

        private void UpdateNotification(Guid userId, string content)
        {
            UserId = userId;
            Content = content;
        }
    }
}
