using System;

namespace Shared.Dto.Principals
{
    public class PrincipalContactCreateDto
    {
        public int PrincipalId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class PrincipalContactUpdateDto
    {
        public int PrincipalId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class PrincipalContactResponseDto
    {
        public int Id { get; set; }
        public int PrincipalId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
