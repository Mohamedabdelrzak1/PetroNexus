using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.VendorRegistrations;
using Shared.Dto.RegistrationDocuments;

namespace Shared.Validators.Registration
{
    public class CreateVendorRegistrationDtoValidator : AbstractValidator<VendorRegistrationCreateDto>
    {
        public CreateVendorRegistrationDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.ClientId).GreaterThan(0);
            RuleFor(x => x.RegistrationNumber).NotEmpty().MaximumLength(100);
            RuleFor(x => x.RegistrationDate).NotEmpty();
            RuleFor(x => x.ExpiryDate).GreaterThan(x => x.RegistrationDate)
                .WithMessage("ExpiryDate must be after RegistrationDate.");
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.RegistrationNumber).MustAsync(async (regNumber, ct) =>
            {
                var existing = await unitOfWork.Repository<VendorRegistration, int>().GetFirstOrDefaultAsync(v => v.RegistrationNumber == regNumber, null, false, ct);
                return existing == null;
            }).WithMessage("Registration number must be unique.");
        }
    }

    public class UpdateVendorRegistrationDtoValidator : AbstractValidator<VendorRegistrationUpdateDto>
    {
        public UpdateVendorRegistrationDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.ClientId).GreaterThan(0);
            RuleFor(x => x.RegistrationNumber).NotEmpty().MaximumLength(100);
            RuleFor(x => x.RegistrationDate).NotEmpty();
            RuleFor(x => x.ExpiryDate).GreaterThan(x => x.RegistrationDate)
                .WithMessage("ExpiryDate must be after RegistrationDate.");
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.RegistrationNumber).MustAsync(async (regNumber, ct) =>
            {
                var existing = await unitOfWork.Repository<VendorRegistration, int>().GetFirstOrDefaultAsync(v => v.RegistrationNumber == regNumber, null, false, ct);
                return existing == null;
            }).WithMessage("Registration number must be unique.");
        }
    }

    public class CreateRegistrationDocumentDtoValidator : AbstractValidator<RegistrationDocumentCreateDto>
    {
        public CreateRegistrationDocumentDtoValidator()
        {
            RuleFor(x => x.VendorRegistrationId).GreaterThan(0);
            RuleFor(x => x.DocumentName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.FilePath).NotEmpty().MaximumLength(500);
        }
    }

    public class UpdateRegistrationDocumentDtoValidator : AbstractValidator<RegistrationDocumentUpdateDto>
    {
        public UpdateRegistrationDocumentDtoValidator()
        {
            RuleFor(x => x.VendorRegistrationId).GreaterThan(0);
            RuleFor(x => x.DocumentName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.FilePath).NotEmpty().MaximumLength(500);
        }
    }
}