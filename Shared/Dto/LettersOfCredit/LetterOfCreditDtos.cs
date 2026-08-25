using System;
using Domain.Enums;

namespace Shared.Dto.LettersOfCredit
{
    public class LetterOfCreditCreateDto
    {
        public string LcNumber { get; set; } = null!;
        public string IssuingBank { get; set; } = null!;
        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsSettled { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class LetterOfCreditUpdateDto
    {
        public string LcNumber { get; set; } = null!;
        public string IssuingBank { get; set; } = null!;
        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsSettled { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class LetterOfCreditResponseDto
    {
        public int Id { get; set; }
        public string LcNumber { get; set; } = null!;
        public string IssuingBank { get; set; } = null!;
        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsSettled { get; set; }
        public int? JournalEntryId { get; set; }
    }
}
