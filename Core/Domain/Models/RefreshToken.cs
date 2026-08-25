using System;
using Domain.Common;

namespace Domain.Models
{
    // توكن التجديد لجلسة الـ JWT — لازم عشان الـ Access Token يفضل عمره قصير بأمان
    public class RefreshToken : BaseEntity<int>
    {
        public string Token { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; } = false;
        public DateTime? RevokedAt { get; set; }

        public string? CreatedByIp { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }

        public bool IsActive => !IsRevoked && DateTime.UtcNow < ExpiresAt;
    }
}
