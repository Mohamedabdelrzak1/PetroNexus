using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.Accounts;
using Shared.Dto.CostCenters;
using Shared.Dto.JournalEntries;
using Shared.Dto.Invoices;
using Shared.Dto.Payments;
using Shared.Dto.ExchangeRates;

namespace Shared.Validators.Finance
{
    public class CreateAccountDtoValidator : AbstractValidator<AccountCreateDto>
    {
        public CreateAccountDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Code).MustAsync(async (code, ct) =>
            {
                var existing = await unitOfWork.Repository<Account, int>().GetFirstOrDefaultAsync(a => a.Code == code, null, false, ct);
                return existing == null;
            }).WithMessage("Account code must be unique.");
        }
    }

    public class UpdateAccountDtoValidator : AbstractValidator<AccountUpdateDto>
    {
        public UpdateAccountDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Code).MustAsync(async (code, ct) =>
            {
                var existing = await unitOfWork.Repository<Account, int>().GetFirstOrDefaultAsync(a => a.Code == code, null, false, ct);
                return existing == null;
            }).WithMessage("Account code must be unique.");
        }
    }

    public class CreateCostCenterDtoValidator : AbstractValidator<CostCenterCreateDto>
    {
        public CreateCostCenterDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Code).MustAsync(async (code, ct) =>
            {
                var existing = await unitOfWork.Repository<CostCenter, int>().GetFirstOrDefaultAsync(c => c.Code == code, null, false, ct);
                return existing == null;
            }).WithMessage("Cost center code must be unique.");
        }
    }

    public class UpdateCostCenterDtoValidator : AbstractValidator<CostCenterUpdateDto>
    {
        public UpdateCostCenterDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Code).MustAsync(async (code, ct) =>
            {
                var existing = await unitOfWork.Repository<CostCenter, int>().GetFirstOrDefaultAsync(c => c.Code == code, null, false, ct);
                return existing == null;
            }).WithMessage("Cost center code must be unique.");
        }
    }

    public class CreateJournalEntryDtoValidator : AbstractValidator<JournalEntryCreateDto>
    {
        public CreateJournalEntryDtoValidator()
        {
            RuleFor(x => x.ReferenceNumber).NotEmpty().MaximumLength(100);
            RuleFor(x => x.EntryDate).NotEmpty();
            RuleFor(x => x.SourceType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Currency).IsInEnum();
        }
    }

    public class UpdateJournalEntryDtoValidator : AbstractValidator<JournalEntryUpdateDto>
    {
        public UpdateJournalEntryDtoValidator()
        {
            RuleFor(x => x.ReferenceNumber).NotEmpty().MaximumLength(100);
            RuleFor(x => x.EntryDate).NotEmpty();
            RuleFor(x => x.SourceType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Currency).IsInEnum();
        }
    }

    public class CreateJournalEntryLineDtoValidator : AbstractValidator<JournalEntryLineCreateDto>
    {
        public CreateJournalEntryLineDtoValidator()
        {
            RuleFor(x => x.AccountId).GreaterThan(0);
            RuleFor(x => x).Must(x => (x.Debit > 0 && x.Credit == 0) || (x.Credit > 0 && x.Debit == 0))
                .WithMessage("Each line must have either Debit or Credit (not both, not neither).");
            RuleFor(x => x.ExchangeRateToBase).GreaterThan(0);
        }
    }

    public class UpdateJournalEntryLineDtoValidator : AbstractValidator<JournalEntryLineUpdateDto>
    {
        public UpdateJournalEntryLineDtoValidator()
        {
            RuleFor(x => x.AccountId).GreaterThan(0);
            RuleFor(x => x).Must(x => (x.Debit > 0 && x.Credit == 0) || (x.Credit > 0 && x.Debit == 0))
                .WithMessage("Each line must have either Debit or Credit (not both, not neither).");
            RuleFor(x => x.ExchangeRateToBase).GreaterThan(0);
        }
    }

    public class CreateInvoiceDtoValidator : AbstractValidator<InvoiceCreateDto>
    {
        public CreateInvoiceDtoValidator()
        {
            RuleFor(x => x.ClientId).GreaterThan(0);
            RuleFor(x => x.Currency).IsInEnum();
            RuleFor(x => x.RetentionPercentage).InclusiveBetween(0, 100);
        }
    }

    public class UpdateInvoiceDtoValidator : AbstractValidator<InvoiceUpdateDto>
    {
        public UpdateInvoiceDtoValidator()
        {
            RuleFor(x => x.ClientId).GreaterThan(0);
            RuleFor(x => x.Currency).IsInEnum();
            RuleFor(x => x.RetentionPercentage).InclusiveBetween(0, 100);
        }
    }

    public class CreatePaymentDtoValidator : AbstractValidator<PaymentCreateDto>
    {
        public CreatePaymentDtoValidator()
        {
            RuleFor(x => x.InvoiceId).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Method).NotEmpty().MaximumLength(100);
        }
    }

    public class UpdatePaymentDtoValidator : AbstractValidator<PaymentUpdateDto>
    {
        public UpdatePaymentDtoValidator()
        {
            RuleFor(x => x.InvoiceId).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Method).NotEmpty().MaximumLength(100);
        }
    }

    public class CreateExchangeRateDtoValidator : AbstractValidator<ExchangeRateCreateDto>
    {
        public CreateExchangeRateDtoValidator()
        {
            RuleFor(x => x.Rate).GreaterThan(0);
            RuleFor(x => x.FromCurrency).IsInEnum();
            RuleFor(x => x.ToCurrency).IsInEnum();
            RuleFor(x => x).Must(x => x.FromCurrency != x.ToCurrency)
                .WithMessage("FromCurrency and ToCurrency must be different.");
        }
    }

    public class UpdateExchangeRateDtoValidator : AbstractValidator<ExchangeRateUpdateDto>
    {
        public UpdateExchangeRateDtoValidator()
        {
            RuleFor(x => x.Rate).GreaterThan(0);
            RuleFor(x => x.FromCurrency).IsInEnum();
            RuleFor(x => x.ToCurrency).IsInEnum();
            RuleFor(x => x).Must(x => x.FromCurrency != x.ToCurrency)
                .WithMessage("FromCurrency and ToCurrency must be different.");
        }
    }
}