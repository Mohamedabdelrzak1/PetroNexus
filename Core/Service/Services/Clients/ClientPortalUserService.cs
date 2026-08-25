using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IClients;
using Shared.Dto.Clients;

namespace Service.Services.Clients
{
    public class ClientPortalUserService : BaseService<ClientPortalUser, int, ClientPortalUserResponseDto, ClientPortalUserCreateDto, ClientPortalUserUpdateDto>, IClientPortalUserService
    {
        public ClientPortalUserService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ClientPortalUserCreateDto> createValidator, IValidator<ClientPortalUserUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
