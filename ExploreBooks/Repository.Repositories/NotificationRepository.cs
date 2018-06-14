using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Persistence;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _databaseService;

        public NotificationRepository(ApplicationDbContext databaseService)
        {
            _databaseService = databaseService;
        }

        public IReadOnlyList<Notification> GetAllNotifications()
        {
            return _databaseService.Notifications.ToList();
        }

        public Notification GetNotificationById(Guid id)
        {
            return _databaseService.Notifications.SingleOrDefault(notification => notification.Id == id);
        }

        public void CreateNotification(Notification notification)
        {
            _databaseService.Notifications.Add(notification);

            _databaseService.SaveChanges();
        }

        public void EditNotification(Notification notification)
        {
            _databaseService.Notifications.Update(notification);

            _databaseService.SaveChanges();
        }

        public void DeleteNotification(Notification notification)
        {
            _databaseService.Notifications.Remove(notification);

            _databaseService.SaveChanges();
        }
    }
}
