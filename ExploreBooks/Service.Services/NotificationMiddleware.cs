using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class NotificationMiddleware : INotificationMiddleware
    {
        private readonly INotificationRepository _repository;

        public NotificationMiddleware(INotificationRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyList<Notification> GetAllNotificationsForUser(string userId)
        {
            return _repository.GetAllNotifications().Where(user => user.UserId == Guid.Parse(userId)).ToList();
        }

        public void DeleteAllNotificationsForUser(string userId)
        {
            var notifications = GetAllNotificationsForUser(userId);

            foreach (var notification in notifications)
            {
                _repository.DeleteNotification(notification);
            }
        }
    }
}
