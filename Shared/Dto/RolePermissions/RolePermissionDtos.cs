using System;

namespace Shared.Dto.RolePermissions
{
    public class RolePermissionCreateDto
    {
        public string RoleId { get; set; } = null!;
        public int PermissionId { get; set; }
    }

    public class RolePermissionUpdateDto
    {
        public string RoleId { get; set; } = null!;
        public int PermissionId { get; set; }
    }

    public class RolePermissionResponseDto
    {
        public int Id { get; set; }
        public string RoleId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public int PermissionId { get; set; }
        public string PermissionCode { get; set; } = null!;
    }
}
