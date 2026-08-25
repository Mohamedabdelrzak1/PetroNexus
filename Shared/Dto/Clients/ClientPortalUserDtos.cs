using System;

namespace Shared.Dto.Clients
{
    public class ClientPortalUserCreateDto
    {
        public int ClientId { get; set; }
        public string AppUserId { get; set; } = null!;
        public bool IsActive { get; set; }
    }

    public class ClientPortalUserUpdateDto
    {
        public int ClientId { get; set; }
        public bool IsActive { get; set; }
    }

    public class ClientPortalUserResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
