using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IShipments;
using Shared.Dto.Shipments;

namespace Service.Services.Shipments
{
    public class ShipmentTrackingEventService : BaseService<ShipmentTrackingEvent, int, ShipmentTrackingEventResponseDto, ShipmentTrackingEventCreateDto, ShipmentTrackingEventUpdateDto>, IShipmentTrackingEventService
    {
        public ShipmentTrackingEventService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ShipmentTrackingEventCreateDto> createValidator, IValidator<ShipmentTrackingEventUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
