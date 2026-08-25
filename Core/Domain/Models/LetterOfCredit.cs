using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class LetterOfCredit : BaseEntity<int>
    {
        public string LcNumber { get; set; }
        public string IssuingBank { get; set; }

        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; } = CurrencyType.USD;

        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public bool IsSettled { get; set; } = false;

        // 📒 القيد اللي بيسجل فتح الاعتماد (Debit: LC Margin/Asset - Credit: Bank)
        public int? JournalEntryId { get; set; }
        public JournalEntry? JournalEntry { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    }
}
