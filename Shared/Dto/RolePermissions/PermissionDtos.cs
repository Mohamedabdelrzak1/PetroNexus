using System;

namespace Shared.Dto.RolePermissions
{
    public class PermissionCreateDto
    {
        public string Code { get; set; } = null!;
        public string? DescriptionAr { get; set; }
        public string? Module { get; set; }
    }

    public class PermissionUpdateDto
    {
        public string Code { get; set; } = null!;
        public string? DescriptionAr { get; set; }
        public string? Module { get; set; }
    }

    public class PermissionResponseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string? DescriptionAr { get; set; }
        public string? Module { get; set; }
    }
}
