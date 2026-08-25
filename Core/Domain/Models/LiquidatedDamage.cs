using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    // غرامة التأخير (LD) المحتملة أو المطبّقة على شحنة متأخرة
    public class LiquidatedDamage : BaseEntity<int>
    {
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; }

        public int DaysDelayed { get; set; }
        public decimal PenaltyPercentagePerDay { get; set; }
        public decimal CalculatedAmount { get; set; }
        public CurrencyType Currency { get; set; } = CurrencyType.USD;

        public LdStatus Status { get; set; } = LdStatus.Threatened;

        public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
    }
}
