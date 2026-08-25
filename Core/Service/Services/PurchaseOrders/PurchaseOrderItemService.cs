using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IPurchaseOrders;
using Shared.Dto.PurchaseOrders;

namespace Service.Services.PurchaseOrders
{
    public class PurchaseOrderItemService : BaseService<PurchaseOrderItem, int, PurchaseOrderItemResponseDto, PurchaseOrderItemCreateDto, PurchaseOrderItemUpdateDto>, IPurchaseOrderItemService
    {
        public PurchaseOrderItemService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<PurchaseOrderItemCreateDto> createValidator, IValidator<PurchaseOrderItemUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
