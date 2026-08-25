using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IPurchaseOrders;
using Shared.Dto.PurchaseOrders;

namespace Service.Services.PurchaseOrders
{
    public class PurchaseOrderService : BaseService<PurchaseOrder, int, PurchaseOrderResponseDto, PurchaseOrderCreateDto, PurchaseOrderUpdateDto>, IPurchaseOrderService
    {
        public PurchaseOrderService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<PurchaseOrderCreateDto> createValidator, IValidator<PurchaseOrderUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
