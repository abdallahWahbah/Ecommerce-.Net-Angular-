using MediatR;

namespace ECommerce.Application.Features.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }
    }
}
