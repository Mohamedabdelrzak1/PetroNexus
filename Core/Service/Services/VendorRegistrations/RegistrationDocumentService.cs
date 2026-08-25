using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IVendorRegistrations;
using Shared.Dto.RegistrationDocuments;

namespace Service.Services.VendorRegistrations
{
    public class RegistrationDocumentService : BaseService<RegistrationDocument, int, RegistrationDocumentResponseDto, RegistrationDocumentCreateDto, RegistrationDocumentUpdateDto>, IRegistrationDocumentService
    {
        public RegistrationDocumentService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RegistrationDocumentCreateDto> createValidator, IValidator<RegistrationDocumentUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
