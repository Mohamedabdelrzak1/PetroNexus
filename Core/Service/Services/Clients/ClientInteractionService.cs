using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IClients;
using Shared.Dto.Clients;


namespace Service.Services.Clients
{
    public class ClientInteractionService : BaseService<ClientInteraction, int, ClientInteractionResponseDto, ClientInteractionCreateDto, ClientInteractionUpdateDto>, IClientInteractionService
    {
        public ClientInteractionService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ClientInteractionCreateDto> createValidator, IValidator<ClientInteractionUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
