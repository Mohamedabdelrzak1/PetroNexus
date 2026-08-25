using FluentValidation;
using Shared.Dto.EngineeringProjects;

namespace Shared.Validators.Engineering
{
    public class CreateEngineeringProjectDtoValidator : AbstractValidator<EngineeringProjectCreateDto>
    {
        public CreateEngineeringProjectDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.ScopeDescription).MaximumLength(2000);
            RuleFor(x => x.TenderId).GreaterThan(0).When(x => x.TenderId.HasValue);
        }
    }

    public class UpdateEngineeringProjectDtoValidator : AbstractValidator<EngineeringProjectUpdateDto>
    {
        public UpdateEngineeringProjectDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.ScopeDescription).MaximumLength(2000);
            RuleFor(x => x.TenderId).GreaterThan(0).When(x => x.TenderId.HasValue);
        }
    }

    public class CreateEngineeringDeliverableDtoValidator : AbstractValidator<EngineeringDeliverableCreateDto>
    {
        public CreateEngineeringDeliverableDtoValidator()
        {
            RuleFor(x => x.EngineeringProjectId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Status).IsInEnum();
        }
    }

    public class UpdateEngineeringDeliverableDtoValidator : AbstractValidator<EngineeringDeliverableUpdateDto>
    {
        public UpdateEngineeringDeliverableDtoValidator()
        {
            RuleFor(x => x.EngineeringProjectId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}