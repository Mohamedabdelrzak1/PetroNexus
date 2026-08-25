using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Models
{
    public class FabricationOrder : BaseEntity<int>
    {
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        public string FabricatorName { get; set; }
        public double? FabricatorRating { get; set; }

        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedFinishDate { get; set; }

        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualFinishDate { get; set; }

        // نسبة الإنجاز الفعلي (تستخدم في لوحة التعجيل: مخطط مقابل فعلي)
        public int CompletionPercentage { get; set; } = 0;

        public decimal? FabricationCost { get; set; }

        // 📒 القيد اللي بيسجل تكلفة التصنيع المحلي المدفوعة للمصنع
        public int? JournalEntryId { get; set; }
        public JournalEntry? JournalEntry { get; set; }

        public ICollection<QualityInspection> Inspections { get; set; } = new List<QualityInspection>();
    }
}
