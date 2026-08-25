using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Shared.Dto.Shipments
{
    public class ShipmentCreateDto
    {
        public int PurchaseOrderId { get; set; }
        public string? TrackingNumber { get; set; }
        public ShipmentStatus Status { get; set; }
        public DateTime? PlannedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public bool IsAtRiskOfDelay { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? CustomsCost { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class ShipmentUpdateDto
    {
        public int PurchaseOrderId { get; set; }
        public string? TrackingNumber { get; set; }
        public ShipmentStatus Status { get; set; }
        public DateTime? PlannedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public bool IsAtRiskOfDelay { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? CustomsCost { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class ShipmentResponseDto
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public string PoNumber { get; set; } = null!;
        public string? TrackingNumber { get; set; }
        public ShipmentStatus Status { get; set; }
        public DateTime? PlannedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public bool IsAtRiskOfDelay { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? CustomsCost { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class ShipmentDetailsDto : ShipmentResponseDto
    {
        public List<ShipmentTrackingEventResponseDto> TrackingEvents { get; set; } = new();
        public List<Shared.Dto.LiquidatedDamages.LiquidatedDamageResponseDto> LiquidatedDamages { get; set; } = new();
    }
}
