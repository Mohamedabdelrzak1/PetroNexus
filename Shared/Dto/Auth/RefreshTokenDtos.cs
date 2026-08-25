using System;

namespace Shared.Dto.Auth
{
    public class RefreshTokenCreateDto
    {
        public string Token { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public string? CreatedByIp { get; set; }
    }

    public class RefreshTokenUpdateDto
    {
        public bool IsRevoked { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
    }

    public class RefreshTokenResponseDto
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string? CreatedByIp { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
        public bool IsActive { get; set; }
    }
}
