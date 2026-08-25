using System;

namespace Shared.Dto.Tenders
{
    public class TenderDocumentAnalysisCreateDto
    {
        public int TenderId { get; set; }
        public string SourceFilePath { get; set; } = null!;
        public string ExtractedSpecifications { get; set; } = null!;
        public int? SuggestedPrincipalId { get; set; }
        public decimal EstimatedMargin { get; set; }
        public DateTime AnalyzedAt { get; set; }
    }

    public class TenderDocumentAnalysisUpdateDto
    {
        public int TenderId { get; set; }
        public string SourceFilePath { get; set; } = null!;
        public string ExtractedSpecifications { get; set; } = null!;
        public int? SuggestedPrincipalId { get; set; }
        public decimal EstimatedMargin { get; set; }
    }

    public class TenderDocumentAnalysisResponseDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public string SourceFilePath { get; set; } = null!;
        public string ExtractedSpecifications { get; set; } = null!;
        public int? SuggestedPrincipalId { get; set; }
        public string? SuggestedPrincipalName { get; set; }
        public decimal EstimatedMargin { get; set; }
        public DateTime AnalyzedAt { get; set; }
    }
}