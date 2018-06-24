using System;
using System.Collections.Generic;
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

        public List<Notification> GetAllNotificationsForUser(string userId)
        {
            return _repository.GetAllNotificationsForUser(Guid.Parse(userId));
        }

        public void DeleteAllNotificationsForUser(string userId)
        {
            var notifications = GetAllNotificationsForUser(userId);

            foreach (var notification in notifications)
            {
                _repository.DeleteNotification(notification);
            }
        }

        public void CreateNotification(Notification notification)
        {
            _repository.CreateNotification(notification);
        }
    }
}
