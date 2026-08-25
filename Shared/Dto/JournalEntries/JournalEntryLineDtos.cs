using System;

namespace Shared.Dto.JournalEntries
{
    public class JournalEntryLineCreateDto
    {
        public int JournalEntryId { get; set; }
        public int AccountId { get; set; }
        public int? CostCenterId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal ExchangeRateToBase { get; set; }
        public decimal DebitBaseCurrency { get; set; }
        public decimal CreditBaseCurrency { get; set; }
    }

    public class JournalEntryLineUpdateDto
    {
        public int JournalEntryId { get; set; }
        public int AccountId { get; set; }
        public int? CostCenterId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal ExchangeRateToBase { get; set; }
        public decimal DebitBaseCurrency { get; set; }
        public decimal CreditBaseCurrency { get; set; }
    }

    public class JournalEntryLineResponseDto
    {
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; } = null!;
        public int? CostCenterId { get; set; }
        public string? CostCenterName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal ExchangeRateToBase { get; set; }
        public decimal DebitBaseCurrency { get; set; }
        public decimal CreditBaseCurrency { get; set; }
    }
}
