using System.Collections.Generic;
using Domain.Common;

namespace Domain.Models
{
    // صلاحية دقيقة أدق من الـ Role العام — مثال: "Tenders.Approve", "Invoices.Delete"
    public class Permission : BaseEntity<int>
    {
        public string Code { get; set; }            // "Tenders.Approve"
        public string? DescriptionAr { get; set; }
        public string? Module { get; set; }          // "Tenders", "Finance", "HR"...

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

    // ربط بين الـ Role (AppRole) والصلاحية
    public class RolePermission : BaseEntity<int>
    {
        public string RoleId { get; set; }
        public AppRole Role { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
