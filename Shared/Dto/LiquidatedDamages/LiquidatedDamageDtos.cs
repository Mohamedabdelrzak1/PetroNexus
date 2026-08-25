using System;
using Domain.Enums;

namespace Shared.Dto.LiquidatedDamages
{
    public class LiquidatedDamageCreateDto
    {
        public int ShipmentId { get; set; }
        public int DaysDelayed { get; set; }
        public decimal PenaltyPercentagePerDay { get; set; }
        public decimal CalculatedAmount { get; set; }
        public CurrencyType Currency { get; set; }
        public LdStatus Status { get; set; }
        public string? Notes { get; set; }
    }

    public class LiquidatedDamageUpdateDto
    {
        public int ShipmentId { get; set; }
        public int DaysDelayed { get; set; }
        public decimal PenaltyPercentagePerDay { get; set; }
        public decimal CalculatedAmount { get; set; }
        public CurrencyType Currency { get; set; }
        public LdStatus Status { get; set; }
        public string? Notes { get; set; }
    }

    public class LiquidatedDamageResponseDto
    {
        public int Id { get; set; }
        public int ShipmentId { get; set; }
        public int DaysDelayed { get; set; }
        public decimal PenaltyPercentagePerDay { get; set; }
        public decimal CalculatedAmount { get; set; }
        public CurrencyType Currency { get; set; }
        public LdStatus Status { get; set; }
        public DateTime CalculatedAt { get; set; }
        public string? Notes { get; set; }
    }
}
