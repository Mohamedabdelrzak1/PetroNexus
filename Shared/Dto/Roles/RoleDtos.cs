namespace Shared.Dto.Roles
{
    public class RoleResponseDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class RoleLookupDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

    public class RoleCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class RoleUpdateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}