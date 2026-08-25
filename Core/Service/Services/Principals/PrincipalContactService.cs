using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IPrincipals;
using Shared.Dto.Principals;

namespace Service.Services.Principals
{
    public class PrincipalContactService : BaseService<PrincipalContact, int, PrincipalContactResponseDto, PrincipalContactCreateDto, PrincipalContactUpdateDto>, IPrincipalContactService
    {
        public PrincipalContactService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<PrincipalContactCreateDto> createValidator, IValidator<PrincipalContactUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
