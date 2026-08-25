using FluentValidation;
using Shared.Dto.Shipments;
using Shared.Dto.LiquidatedDamages;

namespace Shared.Validators.Logistics
{
    public class CreateShipmentDtoValidator : AbstractValidator<ShipmentCreateDto>
    {
        public CreateShipmentDtoValidator()
        {
            RuleFor(x => x.PurchaseOrderId).GreaterThan(0);
            RuleFor(x => x.TrackingNumber).MaximumLength(100);
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.ShippingCost).GreaterThanOrEqualTo(0).When(x => x.ShippingCost.HasValue);
            RuleFor(x => x.CustomsCost).GreaterThanOrEqualTo(0).When(x => x.CustomsCost.HasValue);
        }
    }

    public class UpdateShipmentDtoValidator : AbstractValidator<ShipmentUpdateDto>
    {
        public UpdateShipmentDtoValidator()
        {
            RuleFor(x => x.PurchaseOrderId).GreaterThan(0);
            RuleFor(x => x.TrackingNumber).MaximumLength(100);
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.ShippingCost).GreaterThanOrEqualTo(0).When(x => x.ShippingCost.HasValue);
            RuleFor(x => x.CustomsCost).GreaterThanOrEqualTo(0).When(x => x.CustomsCost.HasValue);
        }
    }

    public class CreateShipmentTrackingEventDtoValidator : AbstractValidator<ShipmentTrackingEventCreateDto>
    {
        public CreateShipmentTrackingEventDtoValidator()
        {
            RuleFor(x => x.ShipmentId).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Location).MaximumLength(200);
        }
    }

    public class UpdateShipmentTrackingEventDtoValidator : AbstractValidator<ShipmentTrackingEventUpdateDto>
    {
        public UpdateShipmentTrackingEventDtoValidator()
        {
            RuleFor(x => x.ShipmentId).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Location).MaximumLength(200);
        }
    }

    public class CreateLiquidatedDamageDtoValidator : AbstractValidator<LiquidatedDamageCreateDto>
    {
        public CreateLiquidatedDamageDtoValidator()
        {
            RuleFor(x => x.ShipmentId).GreaterThan(0);
            RuleFor(x => x.CalculatedAmount).GreaterThan(0);
            RuleFor(x => x.Status).IsInEnum();
        }
    }

    public class UpdateLiquidatedDamageDtoValidator : AbstractValidator<LiquidatedDamageUpdateDto>
    {
        public UpdateLiquidatedDamageDtoValidator()
        {
            RuleFor(x => x.ShipmentId).GreaterThan(0);
            RuleFor(x => x.CalculatedAmount).GreaterThan(0);
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}