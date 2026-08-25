using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class Quotation : BaseEntity<int>
    {
        public int TenderId { get; set; }
        public Tender Tender { get; set; }

        public int PrincipalId { get; set; }
        public Principal Principal { get; set; }

        public string QuotationNumber { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime? ValidUntil { get; set; }

        public QuotationStatus Status { get; set; } = QuotationStatus.Draft;

        public CurrencyType Currency { get; set; } = CurrencyType.USD;

        public ICollection<QuotationItem> Items { get; set; } = new List<QuotationItem>();

        // 🔥 حساب الهامش الإجمالي فوريًا من مجموع بنود العرض
        public decimal TotalClientPrice => Items.Sum(i => i.UnitPriceToClient * i.Quantity);
        public decimal TotalPrincipalCost => Items.Sum(i => i.UnitCostFromPrincipal * i.Quantity);
        public decimal EstimatedMargin => TotalClientPrice - TotalPrincipalCost;
    }

    public class QuotationItem : BaseEntity<int>
    {
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }

        public int PrincipalProductId { get; set; }
        public PrincipalProduct PrincipalProduct { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPriceToClient { get; set; }
        public decimal UnitCostFromPrincipal { get; set; }

        public decimal LineMargin => (UnitPriceToClient - UnitCostFromPrincipal) * Quantity;
    }
}
