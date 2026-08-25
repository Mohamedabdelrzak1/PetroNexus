using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IClients;
using Shared.Dto.Clients;

namespace Service.Services.Clients
{
    public class ClientService : BaseService<Client, int, ClientResponseDto, ClientCreateDto, ClientUpdateDto>, IClientService
    {
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ClientCreateDto> createValidator, IValidator<ClientUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
