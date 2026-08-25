using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IQuotations;
using Shared.Dto.Quotations;

namespace Service.Services.Quotations
{
    public class QuotationItemService : BaseService<QuotationItem, int, QuotationItemResponseDto, QuotationItemCreateDto, QuotationItemUpdateDto>, IQuotationItemService
    {
        public QuotationItemService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<QuotationItemCreateDto> createValidator, IValidator<QuotationItemUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
