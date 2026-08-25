using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Shared.Dto.Quotations
{
    public class QuotationCreateDto
    {
        public int TenderId { get; set; }
        public int PrincipalId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal TotalValue { get; set; }
        public CurrencyType Currency { get; set; }
        public string? Notes { get; set; }
        public QuotationStatus Status { get; set; }
    }

    public class QuotationUpdateDto
    {
        public int TenderId { get; set; }
        public int PrincipalId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal TotalValue { get; set; }
        public CurrencyType Currency { get; set; }
        public string? Notes { get; set; }
        public QuotationStatus Status { get; set; }
    }

    public class QuotationResponseDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public string TenderTitle { get; set; } = null!;
        public int PrincipalId { get; set; }
        public string PrincipalName { get; set; } = null!;
        public DateTime SubmissionDate { get; set; }
        public decimal TotalValue { get; set; }
        public CurrencyType Currency { get; set; }
        public string? Notes { get; set; }
        public QuotationStatus Status { get; set; }
    }

    public class QuotationDetailsDto : QuotationResponseDto
    {
        public List<QuotationItemResponseDto> Items { get; set; } = new();
    }
}
