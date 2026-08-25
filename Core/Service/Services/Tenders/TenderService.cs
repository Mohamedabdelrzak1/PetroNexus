using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.ITenders;
using Shared.Dto.Tenders;

namespace Service.Services.Tenders
{
    public class TenderService : BaseService<Tender, int, TenderResponseDto, TenderCreateDto, TenderUpdateDto>, ITenderService
    {
        public TenderService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<TenderCreateDto> createValidator, IValidator<TenderUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
