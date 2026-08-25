using System;
using Domain.Enums;

namespace Shared.Dto.EscalationLogs
{
    public class EscalationLogCreateDto
    {
        public string RelatedEntityType { get; set; } = null!;
        public int RelatedEntityId { get; set; }
        public EscalationLevel Level { get; set; }
        public NotificationChannel Channel { get; set; }
        public string Message { get; set; } = null!;
        public string? SentToUserId { get; set; }
        public bool WasAcknowledged { get; set; }
    }

    public class EscalationLogUpdateDto
    {
        public bool WasAcknowledged { get; set; }
    }

    public class EscalationLogResponseDto
    {
        public int Id { get; set; }
        public string RelatedEntityType { get; set; } = null!;
        public int RelatedEntityId { get; set; }
        public EscalationLevel Level { get; set; }
        public NotificationChannel Channel { get; set; }
        public string Message { get; set; } = null!;
        public string? SentToUserId { get; set; }
        public DateTime SentAt { get; set; }
        public bool WasAcknowledged { get; set; }
    }
}
