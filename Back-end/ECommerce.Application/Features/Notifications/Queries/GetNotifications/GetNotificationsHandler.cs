using ECommerce.Application.Features.Notifications.Dtos;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Notifications.Queries.GetNotifications
{
    public class GetNotificationsHandler : IRequestHandler<GetNotificationsQuery, IEnumerable<NotificationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNotificationsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<NotificationDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _unitOfWork
            .Repository<Notification>()
            .FindAsync(n => n.UserId == request.UserId);

            return notifications
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt
                })
                .ToList();
        }
    }
}
