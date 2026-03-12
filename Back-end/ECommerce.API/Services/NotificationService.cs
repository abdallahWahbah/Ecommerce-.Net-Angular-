using ECommerce.API.Hubs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ECommerce.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }

        public async Task SendAsync(Guid userId, object payload)
        {
            await _hub.Clients
                .User(userId.ToString())
                .SendAsync("ReceiveNotification", payload);
        }
    }
}
