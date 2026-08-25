using System;
using Domain.Enums;

namespace Shared.Dto.Clients
{
    public class ClientInteractionCreateDto
    {
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime InteractionDate { get; set; }
        public InteractionType Type { get; set; }
        public string Notes { get; set; } = null!;
    }

    public class ClientInteractionUpdateDto
    {
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime InteractionDate { get; set; }
        public InteractionType Type { get; set; }
        public string Notes { get; set; } = null!;
    }

    public class ClientInteractionResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public DateTime InteractionDate { get; set; }
        public InteractionType Type { get; set; }
        public string Notes { get; set; } = null!;
    }
}
