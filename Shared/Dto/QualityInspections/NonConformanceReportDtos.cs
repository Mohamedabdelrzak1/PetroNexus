using System;

namespace Shared.Dto.NonConformanceReports
{
    public class NonConformanceReportCreateDto
    {
        public int QualityInspectionId { get; set; }
        public string IssueDescription { get; set; } = null!;
        public string? CorrectiveAction { get; set; }
        public bool IsResolved { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }

    public class NonConformanceReportUpdateDto
    {
        public int QualityInspectionId { get; set; }
        public string IssueDescription { get; set; } = null!;
        public string? CorrectiveAction { get; set; }
        public bool IsResolved { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }

    public class NonConformanceReportResponseDto
    {
        public int Id { get; set; }
        public int QualityInspectionId { get; set; }
        public string IssueDescription { get; set; } = null!;
        public string? CorrectiveAction { get; set; }
        public bool IsResolved { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}
