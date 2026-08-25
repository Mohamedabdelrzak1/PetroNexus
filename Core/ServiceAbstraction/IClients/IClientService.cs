using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Clients;

namespace ServiceAbstraction.IClients
{
    public interface IClientService : IBaseService<int, ClientResponseDto, ClientCreateDto, ClientUpdateDto>
    {
    }
}
