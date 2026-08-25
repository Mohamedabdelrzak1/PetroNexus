using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class Shipment : BaseEntity<int>
    {
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        public string? TrackingNumber { get; set; }
        public ShipmentStatus Status { get; set; } = ShipmentStatus.AwaitingFabrication;

        public DateTime? PlannedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }

        // 🔔 يستخدم في لوحة التعجيل لتنبيه الفريق قبل ما التأخير يتحول لغرامة (LD)
        public bool IsAtRiskOfDelay { get; set; } = false;

        public decimal? ShippingCost { get; set; }
        public decimal? CustomsCost { get; set; }

        // 📒 القيد اللي بيسجل تكلفة الشحن والجمارك الفعلية (Debit: Expense - Credit: Cash/Bank)
        public int? JournalEntryId { get; set; }
        public JournalEntry? JournalEntry { get; set; }

        public ICollection<ShipmentTrackingEvent> TrackingEvents { get; set; } = new List<ShipmentTrackingEvent>();

        // 🆕 غرامات التأخير المرتبطة بالشحنة دي
        public ICollection<LiquidatedDamage> LiquidatedDamages { get; set; } = new List<LiquidatedDamage>();
    }

    public class ShipmentTrackingEvent : BaseEntity<int>
    {
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; }

        public DateTime EventDate { get; set; } = DateTime.UtcNow;
        public string Description { get; set; } // "غادر المصنع", "وصل الجمارك", "تسليم نهائي"...
        public string? Location { get; set; }
    }
}
