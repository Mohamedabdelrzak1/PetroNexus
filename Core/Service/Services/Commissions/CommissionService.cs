using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.ICommissions;
using Shared.Dto.Commissions;

namespace Service.Services.Commissions
{
    public class CommissionService : BaseService<Commission, int, CommissionResponseDto, CommissionCreateDto, CommissionUpdateDto>, ICommissionService
    {
        public CommissionService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CommissionCreateDto> createValidator, IValidator<CommissionUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
