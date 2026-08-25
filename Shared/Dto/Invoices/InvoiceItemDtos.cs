using System;

namespace Shared.Dto.Invoices
{
    public class InvoiceItemCreateDto
    {
        public int InvoiceId { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class InvoiceItemUpdateDto
    {
        public int InvoiceId { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class InvoiceItemResponseDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
