using ServiceAbstraction.IInvoices;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.Invoices;

namespace Service.Services.Invoices
{
    public class InvoiceItemService : BaseService<InvoiceItem, int, InvoiceItemResponseDto, InvoiceItemCreateDto, InvoiceItemUpdateDto>, IInvoiceItemService
    {
        public InvoiceItemService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<InvoiceItemCreateDto> createValidator, IValidator<InvoiceItemUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
