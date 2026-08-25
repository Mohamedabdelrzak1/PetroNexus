using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IFabricationOrders;
using Shared.Dto.FabricationOrders;

namespace Service.Services.FabricationOrders
{
    public class FabricationOrderService : BaseService<FabricationOrder, int, FabricationOrderResponseDto, FabricationOrderCreateDto, FabricationOrderUpdateDto>, IFabricationOrderService
    {
        public FabricationOrderService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<FabricationOrderCreateDto> createValidator, IValidator<FabricationOrderUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
