using System;
using System.Threading;
using System.Threading.Tasks;
using ServiceAbstraction.INotifications;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.Notifications;

namespace Service.Services.Notifications
{
    public class NotificationService : BaseService<Notification, int, NotificationResponseDto, NotificationCreateDto, NotificationUpdateDto>, INotificationService
    {
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<NotificationCreateDto> createValidator, IValidator<NotificationUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }

        /// <summary>Marks a notification as read.</summary>
        public async Task MarkAsReadAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                throw new KeyNotFoundException($"Notification #{id} not found.");

            entity.IsRead = true;
            Repository.Update(entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}