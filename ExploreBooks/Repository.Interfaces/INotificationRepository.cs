using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface INotificationRepository
    {
        List<Notification> GetAllNotifications();
        List<Notification> GetAllNotificationsForUser(Guid userId);
        Notification GetNotificationById(Guid id);
        void CreateNotification(Notification notification);
        void EditNotification(Notification notification);
        void DeleteNotification(Notification notification);
    }
}
