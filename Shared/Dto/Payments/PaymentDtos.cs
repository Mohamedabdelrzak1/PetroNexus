using System;

namespace Shared.Dto.Payments
{
    public class PaymentCreateDto
    {
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string? Method { get; set; }
        public string? ReferenceNumber { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class PaymentUpdateDto
    {
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string? Method { get; set; }
        public string? ReferenceNumber { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class PaymentResponseDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string? Method { get; set; }
        public string? ReferenceNumber { get; set; }
        public int? JournalEntryId { get; set; }
    }
}
