using FluentValidation;
using Shared.Dto.Notifications;
using Shared.Dto.EscalationLogs;

namespace Shared.Validators.Notifications
{
    public class CreateNotificationDtoValidator : AbstractValidator<NotificationCreateDto>
    {
        public CreateNotificationDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Message).NotEmpty().MaximumLength(2000);
            RuleFor(x => x.TargetUserId).NotEmpty();
            RuleFor(x => x.Channel).IsInEnum();
        }
    }

    public class UpdateNotificationDtoValidator : AbstractValidator<NotificationUpdateDto>
    {
        public UpdateNotificationDtoValidator()
        {
            RuleFor(x => x.IsRead).NotNull();
        }
    }

    public class CreateEscalationLogDtoValidator : AbstractValidator<EscalationLogCreateDto>
    {
        public CreateEscalationLogDtoValidator()
        {
            RuleFor(x => x.RelatedEntityType).NotEmpty().MaximumLength(100);
            RuleFor(x => x.RelatedEntityId).GreaterThan(0);
            RuleFor(x => x.Level).IsInEnum();
            RuleFor(x => x.Message).NotEmpty().MaximumLength(2000);
        }
    }

    public class UpdateEscalationLogDtoValidator : AbstractValidator<EscalationLogUpdateDto>
    {
        public UpdateEscalationLogDtoValidator()
        {
            RuleFor(x => x.WasAcknowledged).NotNull();
        }
    }
}