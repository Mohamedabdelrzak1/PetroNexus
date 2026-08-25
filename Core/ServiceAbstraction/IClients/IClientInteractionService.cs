using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Clients;

namespace ServiceAbstraction.IClients
{
    public interface IClientInteractionService : IBaseService<int, ClientInteractionResponseDto, ClientInteractionCreateDto, ClientInteractionUpdateDto>
    {
    }
}
