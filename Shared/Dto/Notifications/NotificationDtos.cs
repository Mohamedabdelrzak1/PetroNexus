using System;
using Domain.Enums;

namespace Shared.Dto.Notifications
{
    public class NotificationCreateDto
    {
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public NotificationChannel Channel { get; set; }
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
        public string? TargetUserId { get; set; }
        public bool IsRead { get; set; }
    }

    public class NotificationUpdateDto
    {
        public bool IsRead { get; set; }
    }

    public class NotificationResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public NotificationChannel Channel { get; set; }
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
        public string? TargetUserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
