using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.RolePermissions;
using Shared.Dto.AuditLogs;
using Shared.Dto.CompanySettings;

namespace Shared.Validators.System
{
    public class CreatePermissionDtoValidator : AbstractValidator<PermissionCreateDto>
    {
        public CreatePermissionDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Code).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Code).MustAsync(async (code, ct) =>
            {
                var existing = await unitOfWork.Repository<Permission, int>().GetFirstOrDefaultAsync(p => p.Code == code, null, false, ct);
                return existing == null;
            }).WithMessage("Permission code must be unique.");
        }
    }

    public class UpdatePermissionDtoValidator : AbstractValidator<PermissionUpdateDto>
    {
        public UpdatePermissionDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Code).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Code).MustAsync(async (code, ct) =>
            {
                var existing = await unitOfWork.Repository<Permission, int>().GetFirstOrDefaultAsync(p => p.Code == code, null, false, ct);
                return existing == null;
            }).WithMessage("Permission code must be unique.");
        }
    }

    public class CreateRolePermissionDtoValidator : AbstractValidator<RolePermissionCreateDto>
    {
        public CreateRolePermissionDtoValidator()
        {
            RuleFor(x => x.RoleId).NotEmpty();
            RuleFor(x => x.PermissionId).GreaterThan(0);
        }
    }

    public class UpdateRolePermissionDtoValidator : AbstractValidator<RolePermissionUpdateDto>
    {
        public UpdateRolePermissionDtoValidator()
        {
            RuleFor(x => x.RoleId).NotEmpty();
            RuleFor(x => x.PermissionId).GreaterThan(0);
        }
    }

    public class CreateCompanySettingsDtoValidator : AbstractValidator<CompanySettingsCreateDto>
    {
        public CreateCompanySettingsDtoValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.DefaultCurrency).IsInEnum();
            RuleFor(x => x.Address).MaximumLength(500);
            RuleFor(x => x.Phone).MaximumLength(50);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
        }
    }

    public class UpdateCompanySettingsDtoValidator : AbstractValidator<CompanySettingsUpdateDto>
    {
        public UpdateCompanySettingsDtoValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.DefaultCurrency).IsInEnum();
            RuleFor(x => x.Address).MaximumLength(500);
            RuleFor(x => x.Phone).MaximumLength(50);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
        }
    }

    public class CreateAuditLogDtoValidator : AbstractValidator<AuditLogCreateDto>
    {
        public CreateAuditLogDtoValidator()
        {
            RuleFor(x => x.EntityName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.EntityId).GreaterThan(0);
            RuleFor(x => x.Action).IsInEnum();
        }
    }
}