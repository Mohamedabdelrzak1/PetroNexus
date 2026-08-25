using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Shared.Dto.PurchaseOrders
{
    public class PurchaseOrderCreateDto
    {
        public string PoNumber { get; set; } = null!;
        public int PrincipalId { get; set; }
        public int? TenderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDeliveryDate { get; set; }
        public CurrencyType Currency { get; set; }
        public int? JournalEntryId { get; set; }
        public int? LetterOfCreditId { get; set; }
    }

    public class PurchaseOrderUpdateDto
    {
        public string PoNumber { get; set; } = null!;
        public int PrincipalId { get; set; }
        public int? TenderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDeliveryDate { get; set; }
        public CurrencyType Currency { get; set; }
        public int? JournalEntryId { get; set; }
        public int? LetterOfCreditId { get; set; }
    }

    public class PurchaseOrderResponseDto
    {
        public int Id { get; set; }
        public string PoNumber { get; set; } = null!;
        public int PrincipalId { get; set; }
        public string PrincipalName { get; set; } = null!;
        public int? TenderId { get; set; }
        public string? TenderTitle { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDeliveryDate { get; set; }
        public CurrencyType Currency { get; set; }
        public int? JournalEntryId { get; set; }
        public int? LetterOfCreditId { get; set; }
    }

    public class PurchaseOrderDetailsDto : PurchaseOrderResponseDto
    {
        public List<PurchaseOrderItemResponseDto> Items { get; set; } = new();
        public List<Shared.Dto.Shipments.ShipmentResponseDto> Shipments { get; set; } = new();
    }
}
