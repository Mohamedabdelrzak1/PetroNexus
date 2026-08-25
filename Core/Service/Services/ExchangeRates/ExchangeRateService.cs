using ServiceAbstraction.IExchangeRates;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.ExchangeRates;

namespace Service.Services.ExchangeRates
{
    public class ExchangeRateService : BaseService<ExchangeRate, int, ExchangeRateResponseDto, ExchangeRateCreateDto, ExchangeRateUpdateDto>, IExchangeRateService
    {
        public ExchangeRateService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ExchangeRateCreateDto> createValidator, IValidator<ExchangeRateUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
