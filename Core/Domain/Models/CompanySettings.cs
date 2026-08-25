using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    // صف واحد بيحمل بيانات الشركة نفسها (النظام مبني لشركة واحدة حاليًا)
    public class CompanySettings : BaseEntity<int>
    {
        public string CompanyName { get; set; }
        public string? CompanyNameAr { get; set; }
        public string? LogoPath { get; set; }
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public CurrencyType DefaultCurrency { get; set; } = CurrencyType.EGP;
    }
}
