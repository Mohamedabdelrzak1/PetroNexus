using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class QualityInspection : BaseEntity<int>
    {
        public int FabricationOrderId { get; set; }
        public FabricationOrder FabricationOrder { get; set; }

        public DateTime InspectionDate { get; set; } = DateTime.UtcNow;
        public string InspectorName { get; set; }

        public InspectionResult Result { get; set; } = InspectionResult.Pending;

        public string? Notes { get; set; }

        // مسار صورة/ملف مرفوع من تطبيق الموبايل بالموقع
        public string? PhotoPath { get; set; }

        public ICollection<NonConformanceReport> NonConformanceReports { get; set; } = new List<NonConformanceReport>();
    }

    public class NonConformanceReport : BaseEntity<int>
    {
        public int QualityInspectionId { get; set; }
        public QualityInspection QualityInspection { get; set; }

        public string IssueDescription { get; set; }
        public string? CorrectiveAction { get; set; }

        public bool IsResolved { get; set; } = false;
        public DateTime? ResolvedAt { get; set; }
    }
}
