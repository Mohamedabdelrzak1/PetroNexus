using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IVendorRegistrations;
using Shared.Dto.VendorRegistrations;

namespace Service.Services.VendorRegistrations
{
    public class VendorRegistrationService : BaseService<VendorRegistration, int, VendorRegistrationResponseDto, VendorRegistrationCreateDto, VendorRegistrationUpdateDto>, IVendorRegistrationService
    {
        public VendorRegistrationService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<VendorRegistrationCreateDto> createValidator, IValidator<VendorRegistrationUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
