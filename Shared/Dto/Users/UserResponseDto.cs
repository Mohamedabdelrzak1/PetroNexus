using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.Users
{
    public class UserResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public int NumericId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
        public IList<string> Role { get; set; } = new List<string>();

        public bool IsActive { get; set; }

        // ⭐ حالة الظهور
        public bool IsOnline { get; set; }

        // ⭐ آخر تسجيل دخول
        public DateTime? LastLogin { get; set; }

        // ⭐ آخر ظهور (الخروج / انقطاع الاتصال)
        public DateTime? LastSeen { get; set; }

        public string Presence { get; set; } = "";

    }

}
