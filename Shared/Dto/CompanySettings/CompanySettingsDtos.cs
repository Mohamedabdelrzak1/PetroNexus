using Domain.Enums;

namespace Shared.Dto.CompanySettings
{
    public class CompanySettingsCreateDto
    {
        public string CompanyName { get; set; } = null!;
        public string? CompanyNameAr { get; set; }
        public string? LogoPath { get; set; }
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public CurrencyType DefaultCurrency { get; set; } = CurrencyType.EGP;
    }

    public class CompanySettingsUpdateDto
    {
        public string CompanyName { get; set; } = null!;
        public string? CompanyNameAr { get; set; }
        public string? LogoPath { get; set; }
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public CurrencyType DefaultCurrency { get; set; } = CurrencyType.EGP;
    }

    public class CompanySettingsResponseDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? CompanyNameAr { get; set; }
        public string? LogoPath { get; set; }
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public CurrencyType DefaultCurrency { get; set; }
    }
}