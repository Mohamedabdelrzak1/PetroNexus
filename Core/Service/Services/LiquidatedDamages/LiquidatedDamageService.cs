using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.ILiquidatedDamages;
using Shared.Dto.LiquidatedDamages;

namespace Service.Services.LiquidatedDamages
{
    public class LiquidatedDamageService : BaseService<LiquidatedDamage, int, LiquidatedDamageResponseDto, LiquidatedDamageCreateDto, LiquidatedDamageUpdateDto>, ILiquidatedDamageService
    {
        public LiquidatedDamageService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<LiquidatedDamageCreateDto> createValidator, IValidator<LiquidatedDamageUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
