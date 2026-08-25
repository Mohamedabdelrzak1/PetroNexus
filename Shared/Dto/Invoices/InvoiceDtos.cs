using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Shared.Dto.Invoices
{
    public class InvoiceCreateDto
    {
        public string InvoiceNumber { get; set; } = null!;
        public int ClientId { get; set; }
        public int? TenderId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public CurrencyType Currency { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal RetentionPercentage { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class InvoiceUpdateDto
    {
        public string InvoiceNumber { get; set; } = null!;
        public int ClientId { get; set; }
        public int? TenderId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public CurrencyType Currency { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal RetentionPercentage { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class InvoiceResponseDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public int? TenderId { get; set; }
        public string? TenderTitle { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public CurrencyType Currency { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal RetentionPercentage { get; set; }
        public int? JournalEntryId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal RemainingBalance { get; set; }
    }

    public class InvoiceDetailsDto : InvoiceResponseDto
    {
        public List<InvoiceItemResponseDto> Items { get; set; } = new();
        public List<Shared.Dto.Payments.PaymentResponseDto> Payments { get; set; } = new();
    }
}
