using System;
using Domain.Enums;

namespace Shared.Dto.ExchangeRates
{
    public class ExchangeRateCreateDto
    {
        public CurrencyType FromCurrency { get; set; }
        public CurrencyType ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    public class ExchangeRateUpdateDto
    {
        public CurrencyType FromCurrency { get; set; }
        public CurrencyType ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    public class ExchangeRateResponseDto
    {
        public int Id { get; set; }
        public CurrencyType FromCurrency { get; set; }
        public CurrencyType ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
