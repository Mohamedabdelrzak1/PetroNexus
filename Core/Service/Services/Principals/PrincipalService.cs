using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IPrincipals;
using Shared.Dto.Principals;

namespace Service.Services.Principals
{
    public class PrincipalService : BaseService<Principal, int, PrincipalResponseDto, PrincipalCreateDto, PrincipalUpdateDto>, IPrincipalService
    {
        public PrincipalService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<PrincipalCreateDto> createValidator, IValidator<PrincipalUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
