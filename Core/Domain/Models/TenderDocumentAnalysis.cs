using System;
using Domain.Common;

namespace Domain.Models
{
    // نتيجة تحليل ملف المناقصة (PDF) بالذكاء الاصطناعي — علاقة واحد لواحد مع Tender
    public class TenderDocumentAnalysis : BaseEntity<int>
    {
        public int TenderId { get; set; }
        public Tender Tender { get; set; }

        public string SourceFilePath { get; set; }

        public string? ExtractedSpecifications { get; set; }   // JSON/نص المواصفات الفنية المستخرجة
        public DateTime? ExtractedDeadline { get; set; }
        public string? ExtractedTermsSummary { get; set; }

        public int? SuggestedPrincipalId { get; set; }
        public Principal? SuggestedPrincipal { get; set; }

        public decimal? EstimatedMargin { get; set; }   // تقدير مبدئي بناءً على صفقات سابقة مشابهة

        public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
    }
}
