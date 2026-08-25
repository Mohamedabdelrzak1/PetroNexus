using ServiceAbstraction.ICostCenters;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.CostCenters;

namespace Service.Services.CostCenters
{
    public class CostCenterService : BaseService<CostCenter, int, CostCenterResponseDto, CostCenterCreateDto, CostCenterUpdateDto>, ICostCenterService
    {
        public CostCenterService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CostCenterCreateDto> createValidator, IValidator<CostCenterUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
