using ECommerce.Application.Features.Notifications.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Notifications.Queries.GetNotifications
{
    public class GetNotificationsQuery : IRequest<IEnumerable<NotificationDto>>
    {
        public Guid UserId { get; set; }

        public GetNotificationsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
