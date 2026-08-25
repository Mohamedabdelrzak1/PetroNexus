using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IShipments;
using Shared.Dto.Shipments;

namespace Service.Services.Shipments
{
    public class ShipmentService : BaseService<Shipment, int, ShipmentResponseDto, ShipmentCreateDto, ShipmentUpdateDto>, IShipmentService
    {
        public ShipmentService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ShipmentCreateDto> createValidator, IValidator<ShipmentUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
