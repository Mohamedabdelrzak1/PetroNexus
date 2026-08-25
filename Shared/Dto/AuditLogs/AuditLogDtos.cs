using System;
using Domain.Enums;

namespace Shared.Dto.AuditLogs
{
    public class AuditLogCreateDto
    {
        public string EntityName { get; set; } = null!;
        public int EntityId { get; set; }
        public AuditAction Action { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? UserId { get; set; }
    }

    public class AuditLogResponseDto
    {
        public int Id { get; set; }
        public string EntityName { get; set; } = null!;
        public int EntityId { get; set; }
        public AuditAction Action { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
