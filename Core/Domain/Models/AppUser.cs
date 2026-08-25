using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }

        public int NumericId { get; set; }

        public bool IsOnline { get; set; } = false;

        public DateTime? LastLogin { get; set; }
        public DateTime? LastSeen { get; set; }

        // 🆕 جلسات الـ JWT (Refresh Tokens) الخاصة بالمستخدم
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
