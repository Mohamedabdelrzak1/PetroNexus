using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.Commissions;
using Shared.Dto.Principals;

namespace Shared.Validators.Agency
{
    public class CreatePrincipalDtoValidator : AbstractValidator<PrincipalCreateDto>
    {
        public CreatePrincipalDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Country).MaximumLength(100);
            RuleFor(x => x.Website).MaximumLength(200);
            RuleFor(x => x.PerformanceScore).InclusiveBetween(0, 100);
        }
    }

    public class UpdatePrincipalDtoValidator : AbstractValidator<PrincipalUpdateDto>
    {
        public UpdatePrincipalDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Country).MaximumLength(100);
            RuleFor(x => x.Website).MaximumLength(200);
            RuleFor(x => x.PerformanceScore).InclusiveBetween(0, 100);
        }
    }

    public class CreatePrincipalContactDtoValidator : AbstractValidator<PrincipalContactCreateDto>
    {
        public CreatePrincipalContactDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Phone).MaximumLength(50);
        }
    }

    public class UpdatePrincipalContactDtoValidator : AbstractValidator<PrincipalContactUpdateDto>
    {
        public UpdatePrincipalContactDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Phone).MaximumLength(50);
        }
    }

    public class CreatePrincipalProductDtoValidator : AbstractValidator<PrincipalProductCreateDto>
    {
        public CreatePrincipalProductDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.BasePrice).GreaterThanOrEqualTo(0);
        }
    }

    public class UpdatePrincipalProductDtoValidator : AbstractValidator<PrincipalProductUpdateDto>
    {
        public UpdatePrincipalProductDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.BasePrice).GreaterThanOrEqualTo(0);
        }
    }

    public class CreatePrincipalPerformanceReviewDtoValidator : AbstractValidator<PrincipalPerformanceReviewCreateDto>
    {
        public CreatePrincipalPerformanceReviewDtoValidator()
        {
            RuleFor(x => x.OverallScore).InclusiveBetween(0, 100);
            RuleFor(x => x.Comments).MaximumLength(1000);
        }
    }

    public class UpdatePrincipalPerformanceReviewDtoValidator : AbstractValidator<PrincipalPerformanceReviewUpdateDto>
    {
        public UpdatePrincipalPerformanceReviewDtoValidator()
        {
            RuleFor(x => x.OverallScore).InclusiveBetween(0, 100);
            RuleFor(x => x.Comments).MaximumLength(1000);
        }
    }

    public class CreateCommissionDtoValidator : AbstractValidator<CommissionCreateDto>
    {
        public CreateCommissionDtoValidator()
        {
            RuleFor(x => x.CommissionPercentage).InclusiveBetween(0, 100);
            RuleFor(x => x.CommissionValue).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PrincipalId).GreaterThan(0);
            RuleFor(x => x.PurchaseOrderId).GreaterThan(0);
        }
    }

    public class UpdateCommissionDtoValidator : AbstractValidator<CommissionUpdateDto>
    {
        public UpdateCommissionDtoValidator()
        {
            RuleFor(x => x.CommissionPercentage).InclusiveBetween(0, 100);
            RuleFor(x => x.CommissionValue).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PrincipalId).GreaterThan(0);
            RuleFor(x => x.PurchaseOrderId).GreaterThan(0);
        }
    }
}