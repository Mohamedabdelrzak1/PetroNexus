using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    // العمولة المستحقة للشركة من الـ Principal على أوردر شراء معين
    public class Commission : BaseEntity<int>
    {
        public int PrincipalId { get; set; }
        public Principal Principal { get; set; }

        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; } = CurrencyType.USD;

        public CommissionStatus Status { get; set; } = CommissionStatus.Accrued;

        public DateTime AccruedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReceivedAt { get; set; }

        // 📒 القيد اللي بيسجل استلام العمولة فعليًا
        public int? JournalEntryId { get; set; }
        public JournalEntry? JournalEntry { get; set; }
    }
}
