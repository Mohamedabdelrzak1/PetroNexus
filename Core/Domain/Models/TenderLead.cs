using System;
using Domain.Common;

namespace Domain.Models
{
    // فرصة مناقصة مكتشفة تلقائيًا (Web Scraping لبوابة وزارة البترول أو غيرها)
    // قبل ما تتحول لمناقصة رسمية (Tender) جوه النظام
    public class TenderLead : BaseEntity<int>
    {
        public string SourcePortalName { get; set; }     // "بوابة المشتريات الحكومية"...
        public string? SourceUrl { get; set; }
        public string Title { get; set; }

        public DateTime DiscoveredAt { get; set; } = DateTime.UtcNow;
        public DateTime? SubmissionDeadline { get; set; }

        // مطابقة تلقائية مع كتالوج المنتجات المتاح لدينا
        public bool IsRelevantToCatalog { get; set; } = false;
        public double? RelevanceScore { get; set; }        // 0 إلى 1

        public bool IsConverted { get; set; } = false;
        public int? ConvertedTenderId { get; set; }
        public Tender? ConvertedTender { get; set; }
    }
}
