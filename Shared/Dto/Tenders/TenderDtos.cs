using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Shared.Dto.Tenders
{
    public class TenderCreateDto
    {
        public string Title { get; set; } = null!;
        public string? ReferenceNumber { get; set; }
        public int ClientId { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public TenderStatus Status { get; set; }
        public decimal? EstimatedValue { get; set; }
        public CurrencyType Currency { get; set; }
        public string? Notes { get; set; }
        public TenderSource Source { get; set; }
        public int? TenderLeadId { get; set; }
    }

    public class TenderUpdateDto
    {
        public string Title { get; set; } = null!;
        public string? ReferenceNumber { get; set; }
        public int ClientId { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public TenderStatus Status { get; set; }
        public decimal? EstimatedValue { get; set; }
        public CurrencyType Currency { get; set; }
        public string? Notes { get; set; }
        public TenderSource Source { get; set; }
        public int? TenderLeadId { get; set; }
    }

    public class TenderResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? ReferenceNumber { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public DateTime AnnouncementDate { get; set; }
        public DateTime SubmissionDeadline { get; set; }
        public TenderStatus Status { get; set; }
        public decimal? EstimatedValue { get; set; }
        public CurrencyType Currency { get; set; }
        public string? Notes { get; set; }
        public TenderSource Source { get; set; }
        public int? TenderLeadId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TenderDetailsDto : TenderResponseDto
    {
        public List<TenderItemResponseDto> Items { get; set; } = new();
        public TenderDocumentAnalysisResponseDto? DocumentAnalysis { get; set; }
    }
}
