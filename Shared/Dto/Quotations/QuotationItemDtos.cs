using System;

namespace Shared.Dto.Quotations
{
    public class QuotationItemCreateDto
    {
        public int QuotationId { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class QuotationItemUpdateDto
    {
        public int QuotationId { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class QuotationItemResponseDto
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
