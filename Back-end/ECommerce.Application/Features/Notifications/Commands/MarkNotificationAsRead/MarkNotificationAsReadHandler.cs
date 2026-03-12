using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Notifications.Commands.MarkNotificationAsRead
{
    public class MarkNotificationAsReadHandler : IRequestHandler<MarkNotificationAsReadCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkNotificationAsReadHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Notification>();

            var notification = await repo.GetByIdAsync(request.NotificationId);

            if (notification == null)
                return false;

            notification.IsRead = true;

            repo.Update(notification);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
