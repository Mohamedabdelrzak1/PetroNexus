using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IPrincipals;
using Shared.Dto.Principals;

namespace Service.Services.Principals
{
    public class PrincipalProductService : BaseService<PrincipalProduct, int, PrincipalProductResponseDto, PrincipalProductCreateDto, PrincipalProductUpdateDto>, IPrincipalProductService
    {
        public PrincipalProductService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<PrincipalProductCreateDto> createValidator, IValidator<PrincipalProductUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
