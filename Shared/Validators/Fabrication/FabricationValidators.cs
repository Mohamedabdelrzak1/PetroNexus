using FluentValidation;
using Shared.Dto.FabricationOrders;
using Shared.Dto.NonConformanceReports;

namespace Shared.Validators.Fabrication
{
    public class CreateFabricationOrderDtoValidator : AbstractValidator<FabricationOrderCreateDto>
    {
        public CreateFabricationOrderDtoValidator()
        {
            RuleFor(x => x.PurchaseOrderId).GreaterThan(0);
            RuleFor(x => x.FabricatorName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.PlannedStartDate).NotEmpty();
            RuleFor(x => x.PlannedFinishDate).GreaterThan(x => x.PlannedStartDate)
                .WithMessage("PlannedFinishDate must be after PlannedStartDate.");
            RuleFor(x => x.CompletionPercentage).InclusiveBetween(0, 100);
            RuleFor(x => x.FabricationCost).GreaterThanOrEqualTo(0).When(x => x.FabricationCost.HasValue);
        }
    }

    public class UpdateFabricationOrderDtoValidator : AbstractValidator<FabricationOrderUpdateDto>
    {
        public UpdateFabricationOrderDtoValidator()
        {
            RuleFor(x => x.PurchaseOrderId).GreaterThan(0);
            RuleFor(x => x.FabricatorName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.PlannedStartDate).NotEmpty();
            RuleFor(x => x.PlannedFinishDate).GreaterThan(x => x.PlannedStartDate)
                .WithMessage("PlannedFinishDate must be after PlannedStartDate.");
            RuleFor(x => x.CompletionPercentage).InclusiveBetween(0, 100);
            RuleFor(x => x.FabricationCost).GreaterThanOrEqualTo(0).When(x => x.FabricationCost.HasValue);
        }
    }

    public class CreateQualityInspectionDtoValidator : AbstractValidator<QualityInspectionCreateDto>
    {
        public CreateQualityInspectionDtoValidator()
        {
            RuleFor(x => x.FabricationOrderId).GreaterThan(0);
            RuleFor(x => x.InspectorName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Result).IsInEnum();
            RuleFor(x => x.Notes).MaximumLength(2000);
        }
    }

    public class UpdateQualityInspectionDtoValidator : AbstractValidator<QualityInspectionUpdateDto>
    {
        public UpdateQualityInspectionDtoValidator()
        {
            RuleFor(x => x.FabricationOrderId).GreaterThan(0);
            RuleFor(x => x.InspectorName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Result).IsInEnum();
            RuleFor(x => x.Notes).MaximumLength(2000);
        }
    }

    public class CreateNonConformanceReportDtoValidator : AbstractValidator<NonConformanceReportCreateDto>
    {
        public CreateNonConformanceReportDtoValidator()
        {
            RuleFor(x => x.QualityInspectionId).GreaterThan(0);
            RuleFor(x => x.IssueDescription).NotEmpty().MaximumLength(2000);
        }
    }

    public class UpdateNonConformanceReportDtoValidator : AbstractValidator<NonConformanceReportUpdateDto>
    {
        public UpdateNonConformanceReportDtoValidator()
        {
            RuleFor(x => x.QualityInspectionId).GreaterThan(0);
            RuleFor(x => x.IssueDescription).NotEmpty().MaximumLength(2000);
        }
    }
}