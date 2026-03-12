using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Notifications.Queries.UnreadNotificationsCount
{
    public class GetUnreadNotificationsCountQuery : IRequest<int>
    {
        public Guid UserId { get; }

        public GetUnreadNotificationsCountQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
