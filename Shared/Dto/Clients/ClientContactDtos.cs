using System;

namespace Shared.Dto.Clients
{
    public class ClientContactCreateDto
    {
        public int ClientId { get; set; }
        public string FullName { get; set; } = null!;
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class ClientContactUpdateDto
    {
        public int ClientId { get; set; }
        public string FullName { get; set; } = null!;
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class ClientContactResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string FullName { get; set; } = null!;
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsPrimary { get; set; }
    }
}
