using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IQuotations;
using Shared.Dto.Quotations;

namespace Service.Services.Quotations
{
    public class QuotationService : BaseService<Quotation, int, QuotationResponseDto, QuotationCreateDto, QuotationUpdateDto>, IQuotationService
    {
        public QuotationService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<QuotationCreateDto> createValidator, IValidator<QuotationUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
