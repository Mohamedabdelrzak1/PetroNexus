using System;
using Domain.Enums;

namespace Shared.Dto.Commissions
{
    public class CommissionCreateDto
    {
        public int PrincipalId { get; set; }
        public int PurchaseOrderId { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal CommissionValue { get; set; }
        public CurrencyType Currency { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class CommissionUpdateDto
    {
        public int PrincipalId { get; set; }
        public int PurchaseOrderId { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal CommissionValue { get; set; }
        public CurrencyType Currency { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class CommissionResponseDto
    {
        public int Id { get; set; }
        public int PrincipalId { get; set; }
        public string PrincipalName { get; set; } = null!;
        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; } = null!;
        public decimal CommissionPercentage { get; set; }
        public decimal CommissionValue { get; set; }
        public CurrencyType Currency { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public int? JournalEntryId { get; set; }
    }
}
