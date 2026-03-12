using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Notifications.Commands.CreateNotification
{
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public CreateNotificationHandler(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<Guid> Handle(
            CreateNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Notification>();

            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Title = request.Title,
                Message = request.Message,
                Type = request.Type,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await repo.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();

            await _notificationService.SendAsync(request.UserId, new
            {
                notification.Id,
                notification.Title,
                notification.Message,
                notification.Type,
                notification.CreatedAt
            });

            return notification.Id;
        }
    }
}
