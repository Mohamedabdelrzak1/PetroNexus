using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.PurchaseOrders;
using Shared.Dto.LettersOfCredit;

namespace Shared.Validators.Procurement
{
    public class CreatePurchaseOrderDtoValidator : AbstractValidator<PurchaseOrderCreateDto>
    {
        public CreatePurchaseOrderDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.PoNumber).NotEmpty().MaximumLength(100);
            RuleFor(x => x.PrincipalId).GreaterThan(0);
            RuleFor(x => x.Currency).IsInEnum();
            RuleFor(x => x.PoNumber).MustAsync(async (poNumber, ct) =>
            {
                var existing = await unitOfWork.Repository<PurchaseOrder, int>().GetFirstOrDefaultAsync(p => p.PoNumber == poNumber, null, false, ct);
                return existing == null;
            }).WithMessage("PO Number must be unique.");
        }
    }

    public class UpdatePurchaseOrderDtoValidator : AbstractValidator<PurchaseOrderUpdateDto>
    {
        public UpdatePurchaseOrderDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.PoNumber).NotEmpty().MaximumLength(100);
            RuleFor(x => x.PrincipalId).GreaterThan(0);
            RuleFor(x => x.Currency).IsInEnum();
            RuleFor(x => x.PoNumber).MustAsync(async (poNumber, ct) =>
            {
                var existing = await unitOfWork.Repository<PurchaseOrder, int>().GetFirstOrDefaultAsync(p => p.PoNumber == poNumber, null, false, ct);
                return existing == null;
            }).WithMessage("PO Number must be unique.");
        }
    }

    public class CreatePurchaseOrderItemDtoValidator : AbstractValidator<PurchaseOrderItemCreateDto>
    {
        public CreatePurchaseOrderItemDtoValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.UnitCost).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PrincipalProductId).GreaterThan(0);
        }
    }

    public class UpdatePurchaseOrderItemDtoValidator : AbstractValidator<PurchaseOrderItemUpdateDto>
    {
        public UpdatePurchaseOrderItemDtoValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.UnitCost).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PrincipalProductId).GreaterThan(0);
        }
    }

    public class CreateLetterOfCreditDtoValidator : AbstractValidator<LetterOfCreditCreateDto>
    {
        public CreateLetterOfCreditDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.LcNumber).NotEmpty().MaximumLength(100);
            RuleFor(x => x.IssuingBank).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Currency).IsInEnum();
            RuleFor(x => x.IssueDate).NotEmpty();
            RuleFor(x => x.ExpiryDate).GreaterThan(x => x.IssueDate)
                .WithMessage("ExpiryDate must be after IssueDate.");
            RuleFor(x => x.LcNumber).MustAsync(async (lcNumber, ct) =>
            {
                var existing = await unitOfWork.Repository<LetterOfCredit, int>().GetFirstOrDefaultAsync(l => l.LcNumber == lcNumber, null, false, ct);
                return existing == null;
            }).WithMessage("LC Number must be unique.");
        }
    }

    public class UpdateLetterOfCreditDtoValidator : AbstractValidator<LetterOfCreditUpdateDto>
    {
        public UpdateLetterOfCreditDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.LcNumber).NotEmpty().MaximumLength(100);
            RuleFor(x => x.IssuingBank).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Currency).IsInEnum();
            RuleFor(x => x.IssueDate).NotEmpty();
            RuleFor(x => x.ExpiryDate).GreaterThan(x => x.IssueDate)
                .WithMessage("ExpiryDate must be after IssueDate.");
            RuleFor(x => x.LcNumber).MustAsync(async (lcNumber, ct) =>
            {
                var existing = await unitOfWork.Repository<LetterOfCredit, int>().GetFirstOrDefaultAsync(l => l.LcNumber == lcNumber, null, false, ct);
                return existing == null;
            }).WithMessage("LC Number must be unique.");
        }
    }
}