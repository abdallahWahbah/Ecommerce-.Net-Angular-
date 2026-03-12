using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Notifications.Commands.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommand : IRequest<bool>
    {
        public Guid NotificationId { get; }

        public MarkNotificationAsReadCommand(Guid notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
