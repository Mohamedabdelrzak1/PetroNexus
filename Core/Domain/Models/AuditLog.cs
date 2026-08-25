using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class AuditLog : BaseEntity<int>
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }

        public AuditAction Action { get; set; }

        public string? OldValues { get; set; } // JSON snapshot قبل التعديل
        public string? NewValues { get; set; } // JSON snapshot بعد التعديل

        public string? UserId { get; set; }
        public AppUser? User { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
