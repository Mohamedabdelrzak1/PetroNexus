using System;
using System.Collections.Generic;

namespace Shared.Dto.Clients
{
    public class ClientCreateDto
    {
        public string Name { get; set; } = null!;
        public string? NameAr { get; set; }
        public string Sector { get; set; } = null!;
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }
    }

    public class ClientUpdateDto
    {
        public string Name { get; set; } = null!;
        public string? NameAr { get; set; }
        public string Sector { get; set; } = null!;
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }
    }

    public class ClientResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? NameAr { get; set; }
        public string Sector { get; set; } = null!;
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ClientDetailsDto : ClientResponseDto
    {
        public List<ClientContactResponseDto> Contacts { get; set; } = new();
        public List<Shared.Dto.Tenders.TenderResponseDto> Tenders { get; set; } = new();
    }

    public class ClientSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Sector { get; set; } = null!;
    }

    public class ClientLookupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class ClientFilterDto
    {
        public string? Name { get; set; }
        public string? Sector { get; set; }
    }

}
