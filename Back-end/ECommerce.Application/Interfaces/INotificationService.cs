namespace ECommerce.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Guid userId, object payload);
    }
}
