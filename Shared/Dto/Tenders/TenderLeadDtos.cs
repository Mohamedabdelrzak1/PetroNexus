using System;

namespace Shared.Dto.Tenders
{
    public class TenderLeadCreateDto
    {
        public string Title { get; set; } = null!;
        public string SourcePortalName { get; set; } = null!;
        public string? SourceUrl { get; set; }
        public string? RawText { get; set; }
        public DateTime DiscoveredAt { get; set; }
        public bool IsRelevantToCatalog { get; set; }
        public double RelevanceScore { get; set; }
    }

    public class TenderLeadUpdateDto
    {
        public string Title { get; set; } = null!;
        public string SourcePortalName { get; set; } = null!;
        public string? SourceUrl { get; set; }
        public string? RawText { get; set; }
        public bool IsRelevantToCatalog { get; set; }
        public double RelevanceScore { get; set; }
    }

    public class TenderLeadResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string SourcePortalName { get; set; } = null!;
        public string? SourceUrl { get; set; }
        public string? RawText { get; set; }
        public DateTime DiscoveredAt { get; set; }
        public bool IsRelevantToCatalog { get; set; }
        public double RelevanceScore { get; set; }
    }
}