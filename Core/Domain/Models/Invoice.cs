using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    // الفاتورة الفعلية اللي بتتبعت للعميل (منفصلة عن القيد المحاسبي الداخلي)
    public class Invoice : BaseEntity<int>
    {
        public DateTime CreatedAt;

        public string InvoiceNumber { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int? TenderId { get; set; }
        public Tender? Tender { get; set; }

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }

        public CurrencyType Currency { get; set; } = CurrencyType.USD;
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

        // نسبة الاحتجاز (Retention) اللي بتتخصم من قيمة الفاتورة لحد التسليم النهائي
        public decimal RetentionPercentage { get; set; } = 0;

        public int? JournalEntryId { get; set; }
        public JournalEntry? JournalEntry { get; set; }

        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public decimal TotalAmount => Items.Sum(i => i.UnitPrice * i.Quantity);
        public decimal TotalPaid => Payments.Sum(p => p.Amount);
        public decimal RemainingBalance => TotalAmount - TotalPaid;
    }

    public class InvoiceItem : BaseEntity<int>
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class Payment : BaseEntity<int>
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }

        public string? Method { get; set; } // تحويل بنكي، شيك، نقدي...
        public string? ReferenceNumber { get; set; }

        // 📒 القيد اللي بيسجل استلام الدفعة (Debit: Cash/Bank - Credit: Accounts Receivable)
        public int? JournalEntryId { get; set; }
        public JournalEntry? JournalEntry { get; set; }
    }
}
