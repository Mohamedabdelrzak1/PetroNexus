using System;
using Domain.Enums;

namespace Shared.Dto.Principals
{
    public class PrincipalProductCreateDto
    {
        public int PrincipalId { get; set; }
        public string Name { get; set; } = null!;
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public CurrencyType Currency { get; set; }
    }

    public class PrincipalProductUpdateDto
    {
        public int PrincipalId { get; set; }
        public string Name { get; set; } = null!;
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public CurrencyType Currency { get; set; }
    }

    public class PrincipalProductResponseDto
    {
        public int Id { get; set; }
        public int PrincipalId { get; set; }
        public string PrincipalName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public CurrencyType Currency { get; set; }
    }
}
