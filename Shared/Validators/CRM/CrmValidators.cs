using FluentValidation;
using Shared.Dto.Clients;

namespace Shared.Validators.CRM
{
    public class CreateClientDtoValidator : AbstractValidator<ClientCreateDto>
    {
        public CreateClientDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Address).MaximumLength(500);
        }
    }

    public class UpdateClientDtoValidator : AbstractValidator<ClientUpdateDto>
    {
        public UpdateClientDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Address).MaximumLength(500);
        }
    }

    public class CreateClientContactDtoValidator : AbstractValidator<ClientContactCreateDto>
    {
        public CreateClientContactDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Phone).MaximumLength(50);
            RuleFor(x => x.JobTitle).MaximumLength(100);
        }
    }

    public class UpdateClientContactDtoValidator : AbstractValidator<ClientContactUpdateDto>
    {
        public UpdateClientContactDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Phone).MaximumLength(50);
            RuleFor(x => x.JobTitle).MaximumLength(100);
        }
    }

    public class CreateClientInteractionDtoValidator : AbstractValidator<ClientInteractionCreateDto>
    {
        public CreateClientInteractionDtoValidator()
        {
            RuleFor(x => x.ClientId).GreaterThan(0);
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Notes).MaximumLength(2000);
        }
    }

    public class UpdateClientInteractionDtoValidator : AbstractValidator<ClientInteractionUpdateDto>
    {
        public UpdateClientInteractionDtoValidator()
        {
            RuleFor(x => x.ClientId).GreaterThan(0);
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Notes).MaximumLength(2000);
        }
    }

    public class CreateClientPortalUserDtoValidator : AbstractValidator<ClientPortalUserCreateDto>
    {
        public CreateClientPortalUserDtoValidator()
        {
            RuleFor(x => x.ClientId).GreaterThan(0);
            RuleFor(x => x.AppUserId).NotEmpty();
        }
    }

    public class UpdateClientPortalUserDtoValidator : AbstractValidator<ClientPortalUserUpdateDto>
    {
        public UpdateClientPortalUserDtoValidator()
        {
            RuleFor(x => x.ClientId).GreaterThan(0);
        }
    }
}