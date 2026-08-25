using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class AppRole : IdentityRole
    {
        public string? Description { get; set; }

        // 🆕 صلاحيات دقيقة مربوطة بالـ Role ده
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
