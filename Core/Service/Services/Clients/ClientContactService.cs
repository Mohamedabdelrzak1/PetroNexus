using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IClients;
using Shared.Dto.Clients;


namespace Service.Services.Clients
{
    public class ClientContactService : BaseService<ClientContact, int, ClientContactResponseDto, ClientContactCreateDto, ClientContactUpdateDto>, IClientContactService
    {
        public ClientContactService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ClientContactCreateDto> createValidator, IValidator<ClientContactUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
