using FluentValidation;
using Shared.Dto.Tenders;
using Shared.Dto.Quotations;

namespace Shared.Validators.Tenders
{
    public class CreateTenderDtoValidator : AbstractValidator<TenderCreateDto>
    {
        public CreateTenderDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.ClientId).GreaterThan(0);
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Currency).IsInEnum();
        }
    }

    public class UpdateTenderDtoValidator : AbstractValidator<TenderUpdateDto>
    {
        public UpdateTenderDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.ClientId).GreaterThan(0);
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Currency).IsInEnum();
        }
    }

    public class CreateTenderItemDtoValidator : AbstractValidator<TenderItemCreateDto>
    {
        public CreateTenderItemDtoValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.PrincipalProductId).GreaterThan(0).When(x => x.PrincipalProductId.HasValue);
        }
    }

    public class UpdateTenderItemDtoValidator : AbstractValidator<TenderItemUpdateDto>
    {
        public UpdateTenderItemDtoValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.PrincipalProductId).GreaterThan(0).When(x => x.PrincipalProductId.HasValue);
        }
    }

    public class CreateTenderLeadDtoValidator : AbstractValidator<TenderLeadCreateDto>
    {
        public CreateTenderLeadDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.SourcePortalName).NotEmpty().MaximumLength(100);
        }
    }

    public class UpdateTenderLeadDtoValidator : AbstractValidator<TenderLeadUpdateDto>
    {
        public UpdateTenderLeadDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.SourcePortalName).NotEmpty().MaximumLength(100);
        }
    }

    public class CreateTenderDocumentAnalysisDtoValidator : AbstractValidator<TenderDocumentAnalysisCreateDto>
    {
        public CreateTenderDocumentAnalysisDtoValidator()
        {
            RuleFor(x => x.TenderId).GreaterThan(0);
            RuleFor(x => x.SourceFilePath).NotEmpty().MaximumLength(500);
        }
    }

    public class UpdateTenderDocumentAnalysisDtoValidator : AbstractValidator<TenderDocumentAnalysisUpdateDto>
    {
        public UpdateTenderDocumentAnalysisDtoValidator()
        {
            RuleFor(x => x.TenderId).GreaterThan(0);
            RuleFor(x => x.SourceFilePath).NotEmpty().MaximumLength(500);
        }
    }

    public class CreateQuotationDtoValidator : AbstractValidator<QuotationCreateDto>
    {
        public CreateQuotationDtoValidator()
        {
            RuleFor(x => x.TenderId).GreaterThan(0);
            RuleFor(x => x.PrincipalId).GreaterThan(0);
            RuleFor(x => x.TotalValue).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Currency).IsInEnum();
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Notes).MaximumLength(2000);
        }
    }

    public class UpdateQuotationDtoValidator : AbstractValidator<QuotationUpdateDto>
    {
        public UpdateQuotationDtoValidator()
        {
            RuleFor(x => x.TenderId).GreaterThan(0);
            RuleFor(x => x.PrincipalId).GreaterThan(0);
            RuleFor(x => x.TotalValue).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Currency).IsInEnum();
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Notes).MaximumLength(2000);
        }
    }

    public class CreateQuotationItemDtoValidator : AbstractValidator<QuotationItemCreateDto>
    {
        public CreateQuotationItemDtoValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.TotalPrice).GreaterThanOrEqualTo(0);
        }
    }

    public class UpdateQuotationItemDtoValidator : AbstractValidator<QuotationItemUpdateDto>
    {
        public UpdateQuotationItemDtoValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.TotalPrice).GreaterThanOrEqualTo(0);
        }
    }
}