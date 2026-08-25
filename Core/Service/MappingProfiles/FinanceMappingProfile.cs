using AutoMapper;
using Domain.Models;
using Shared.Dto.Accounts;
using Shared.Dto.CompanySettings;
using Shared.Dto.CostCenters;
using Shared.Dto.ExchangeRates;
using Shared.Dto.Invoices;
using Shared.Dto.JournalEntries;
using Shared.Dto.Payments;

namespace Service.MappingProfiles
{
    public class FinanceMappingProfile : Profile
    {
        public FinanceMappingProfile()
        {
            // ==========================================
            // FINANCE
            // ==========================================
            CreateMap<AccountCreateDto, Account>();
            CreateMap<AccountUpdateDto, Account>();
            CreateMap<Account, AccountResponseDto>()
                .ForMember(dest => dest.ParentAccountName, opt => opt.MapFrom(src => src.ParentAccount != null ? src.ParentAccount.Name : string.Empty));

            CreateMap<CostCenterCreateDto, CostCenter>();
            CreateMap<CostCenterUpdateDto, CostCenter>();
            CreateMap<CostCenter, CostCenterResponseDto>();

            CreateMap<JournalEntryCreateDto, JournalEntry>();
            CreateMap<JournalEntryUpdateDto, JournalEntry>();
            CreateMap<JournalEntry, JournalEntryResponseDto>();
            CreateMap<JournalEntry, JournalEntryDetailsDto>()
                .ForMember(dest => dest.Lines, opt => opt.MapFrom(src => src.Lines));

            CreateMap<JournalEntryLineCreateDto, JournalEntryLine>();
            CreateMap<JournalEntryLineUpdateDto, JournalEntryLine>();
            CreateMap<JournalEntryLine, JournalEntryLineResponseDto>()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account != null ? src.Account.Name : string.Empty))
                .ForMember(dest => dest.CostCenterName, opt => opt.MapFrom(src => src.CostCenter != null ? src.CostCenter.Name : string.Empty));

            CreateMap<InvoiceCreateDto, Invoice>();
            CreateMap<InvoiceUpdateDto, Invoice>();
            CreateMap<Invoice, InvoiceResponseDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : string.Empty))
                .ForMember(dest => dest.TenderTitle, opt => opt.MapFrom(src => src.Tender != null ? src.Tender.Title : string.Empty));
            CreateMap<Invoice, InvoiceDetailsDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));

            CreateMap<InvoiceItemCreateDto, InvoiceItem>();
            CreateMap<InvoiceItemUpdateDto, InvoiceItem>();
            CreateMap<InvoiceItem, InvoiceItemResponseDto>();

            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<PaymentUpdateDto, Payment>();
            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.InvoiceNumber : string.Empty));

            CreateMap<ExchangeRateCreateDto, ExchangeRate>();
            CreateMap<ExchangeRateUpdateDto, ExchangeRate>();
            CreateMap<ExchangeRate, ExchangeRateResponseDto>();

            CreateMap<CompanySettingsCreateDto, CompanySettings>();
            CreateMap<CompanySettingsUpdateDto, CompanySettings>();
            CreateMap<CompanySettings, CompanySettingsResponseDto>();
        }
    }
}