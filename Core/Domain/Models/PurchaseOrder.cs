using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class PurchaseOrder : BaseEntity<int>
    {
        public string PoNumber { get; set; }

        public int PrincipalId { get; set; }
        public Principal Principal { get; set; }

        // ربط اختياري بالمناقصة اللي الأوردر ده بيخدمها
        public int? TenderId { get; set; }
        public Tender? Tender { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? RequiredDeliveryDate { get; set; }

        public CurrencyType Currency { get; set; } = CurrencyType.USD;

        // 📒 القيد اللي بيسجل تكلفة الاستيراد (Debit: Inventory/COGS - Credit: AP للـ Principal)
        public int? JournalEntryId { get; set; }
        public JournalEntry? JournalEntry { get; set; }

        public int? LetterOfCreditId { get; set; }
        public LetterOfCredit? LetterOfCredit { get; set; }

        public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
        public FabricationOrder? FabricationOrder { get; set; }

        // 🆕 العمولات المستحقة على أوردر الشراء ده
        public ICollection<Commission> Commissions { get; set; } = new List<Commission>();
    }

    public class PurchaseOrderItem : BaseEntity<int>
    {
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        public int PrincipalProductId { get; set; }
        public PrincipalProduct PrincipalProduct { get; set; }

        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
    }
}
