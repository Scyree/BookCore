using System.Collections.Generic;
using Domain.Data;

namespace Service.Interfaces
{
    public interface INotificationMiddleware
    {
        IReadOnlyList<Notification> GetAllNotificationsForUser(string userId);
        void DeleteAllNotificationsForUser(string userId);
        void CreateNotification(Notification notification);
    }
}
