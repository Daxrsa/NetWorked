
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService _notificationsService;

        public NotificationsController(INotificationsService notificationService)
        {
            _notificationsService = notificationService;
        }
        [HttpGet]
        public async Task<ActionResult<List<NotificationsDTO>>> GetAllNotifications()
        {
            return (await _notificationsService.GetNotifications());
        }
        [HttpPost("add")]
        public async Task<ActionResult<List<NotificationsDTO>>> AddPost(NotificationsDTO notificationsDto)
        {
            return (await _notificationsService.AddNotification(notificationsDto));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationsDTO>> GetNotificationById(Guid id)
        {
            return (await _notificationsService.GetNotificationById(id));
        }
    }
}
