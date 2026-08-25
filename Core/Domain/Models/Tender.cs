using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class Tender : BaseEntity<int>
    {
        public string Title { get; set; }
        public string? ReferenceNumber { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public DateTime AnnouncementDate { get; set; }
        public DateTime SubmissionDeadline { get; set; }

        public TenderStatus Status { get; set; } = TenderStatus.New;

        public decimal? EstimatedValue { get; set; }
        public CurrencyType Currency { get; set; } = CurrencyType.EGP;

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TenderItem> Items { get; set; } = new List<TenderItem>();
        public ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();
        public ICollection<EngineeringProject> EngineeringProjects { get; set; } = new List<EngineeringProject>();

        // مركز التكلفة اللي بيتربط بيه المشروع بمجرد ما المناقصة تتفوز (Won)
        public CostCenter? CostCenter { get; set; }

        // 🆕 مصدر المناقصة: يدوي / سكرابينج بوابة وزارة / استخراج AI
        public TenderSource Source { get; set; } = TenderSource.Manual;

        // 🆕 لو المناقصة جت من فرصة مكتشفة تلقائيًا
        public int? TenderLeadId { get; set; }
        public TenderLead? TenderLead { get; set; }

        // 🆕 نتيجة تحليل ملف المناقصة بالـ AI (لو موجودة)
        public TenderDocumentAnalysis? DocumentAnalysis { get; set; }
    }

    public class TenderItem : BaseEntity<int>
    {
        public int TenderId { get; set; }
        public Tender Tender { get; set; }

        public string Description { get; set; }
        public int Quantity { get; set; }
        public string? UnitOfMeasure { get; set; }

        // ربط اختياري بمنتج جاهز من كتالوج أحد الـ Principals
        public int? PrincipalProductId { get; set; }
        public PrincipalProduct? PrincipalProduct { get; set; }
    }
}
