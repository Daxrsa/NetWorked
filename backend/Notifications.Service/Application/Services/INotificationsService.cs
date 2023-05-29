using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface INotificationsService
    {
        Task<List<NotificationsDTO>> GetNotifications();
        Task<NotificationsDTO> GetNotificationById(Guid id);
        Task<List<NotificationsDTO>> AddNotification(NotificationsDTO notificationsDto);
        Task<List<NotificationsDTO>> UpdateNotification(Guid id, NotificationsDTO postDto);
        Task<List<NotificationsDTO>> DeleteNotification(Guid id);
    }
}
