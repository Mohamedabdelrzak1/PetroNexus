using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class Notification : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public NotificationChannel Channel { get; set; } = NotificationChannel.InApp;

        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }

        public string? TargetUserId { get; set; }
        public AppUser? TargetUser { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
