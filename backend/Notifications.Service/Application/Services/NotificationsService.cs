using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly List<NotificationsDTO> _notifications;

        public NotificationsService()
        {
            _notifications = new List<NotificationsDTO>();
        }

        public Task<List<NotificationsDTO>> GetNotifications()
        {
            // Return all notifications
            return Task.FromResult(_notifications);
        }

        public Task<NotificationsDTO> GetNotificationById(Guid id)
        {
            // Find the notification with the specified ID
            var notification = _notifications.Find(n => n.Id == id);
            return Task.FromResult(notification);
        }

        public Task<List<NotificationsDTO>> AddNotification(NotificationsDTO notificationsDto)
        {
            // Generate a new ID
            notificationsDto.Id = Guid.NewGuid();

            // Add the notification to the list
            _notifications.Add(notificationsDto);

            // Return all notifications
            return Task.FromResult(_notifications);
        }

        public Task<List<NotificationsDTO>> UpdateNotification(Guid id, NotificationsDTO notificationsDto)
        {
            // Find the notification with the specified ID
            var existingNotification = _notifications.Find(n => n.Id == id);
            if (existingNotification == null)
            {
                throw new Exception("Notification not found");
            }

            // Update the notification properties
            existingNotification.Description = notificationsDto.Description;
            existingNotification.NoticationCreated = notificationsDto.NoticationCreated;

            // Return all notifications
            return Task.FromResult(_notifications);
        }

        public Task<List<NotificationsDTO>> DeleteNotification(Guid id)
        {
            // Find the notification with the specified ID
            var existingNotification = _notifications.Find(n => n.Id == id);
            if (existingNotification == null)
            {
                throw new Exception("Notification not found");
            }

            // Remove the notification from the list
            _notifications.Remove(existingNotification);

            // Return all notifications
            return Task.FromResult(_notifications);
        }
    }
}
