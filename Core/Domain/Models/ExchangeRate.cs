using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class ExchangeRate : BaseEntity<int>
    {
        public CurrencyType FromCurrency { get; set; }
        public CurrencyType ToCurrency { get; set; }

        public decimal Rate { get; set; }
        public DateTime EffectiveDate { get; set; } = DateTime.UtcNow;
    }
}
