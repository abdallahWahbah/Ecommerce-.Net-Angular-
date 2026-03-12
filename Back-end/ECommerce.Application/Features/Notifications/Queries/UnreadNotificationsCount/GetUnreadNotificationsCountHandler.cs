using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Notifications.Queries.UnreadNotificationsCount
{
    public class GetUnreadNotificationsCountHandler : IRequestHandler<GetUnreadNotificationsCountQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUnreadNotificationsCountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(
            GetUnreadNotificationsCountQuery request,
            CancellationToken cancellationToken)
        {
            var notifications = await _unitOfWork
                .Repository<Notification>()
                .FindAsync(n => n.UserId == request.UserId && !n.IsRead);

            return notifications.Count();
        }
    }
}
