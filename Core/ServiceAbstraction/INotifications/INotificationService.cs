using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Notifications;

namespace ServiceAbstraction.INotifications
{
    public interface INotificationService : IBaseService<int, NotificationResponseDto, NotificationCreateDto, NotificationUpdateDto>
    {
        /// <summary>Marks a notification as read.</summary>
        Task MarkAsReadAsync(int id, CancellationToken cancellationToken = default);
    }
}
