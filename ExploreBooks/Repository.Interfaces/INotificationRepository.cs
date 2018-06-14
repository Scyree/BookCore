using System;
using System.Collections.Generic;
using Domain.Data;

namespace Repository.Interfaces
{
    public interface INotificationRepository
    {
        IReadOnlyList<Notification> GetAllNotifications();
        Notification GetNotificationById(Guid id);
        void CreateNotification(Notification notification);
        void EditNotification(Notification notification);
        void DeleteNotification(Notification notification);
    }
}
