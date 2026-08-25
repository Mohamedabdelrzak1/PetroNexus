using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.ITenders;
using Shared.Dto.Tenders;

namespace Service.Services.Tenders
{
    public class TenderItemService : BaseService<TenderItem, int, TenderItemResponseDto, TenderItemCreateDto, TenderItemUpdateDto>, ITenderItemService
    {
        public TenderItemService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<TenderItemCreateDto> createValidator, IValidator<TenderItemUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
