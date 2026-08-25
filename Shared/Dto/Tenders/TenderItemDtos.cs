using System;

namespace Shared.Dto.Tenders
{
    public class TenderItemCreateDto
    {
        public int TenderId { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public string? UnitOfMeasure { get; set; }
        public int? PrincipalProductId { get; set; }
    }

    public class TenderItemUpdateDto
    {
        public int TenderId { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public string? UnitOfMeasure { get; set; }
        public int? PrincipalProductId { get; set; }
    }

    public class TenderItemResponseDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public string? UnitOfMeasure { get; set; }
        public int? PrincipalProductId { get; set; }
        public string? PrincipalProductName { get; set; }
    }
}
