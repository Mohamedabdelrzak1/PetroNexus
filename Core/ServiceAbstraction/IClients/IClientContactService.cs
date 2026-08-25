using Shared.Dto.Clients;
using System.Threading;
using System.Threading.Tasks;


namespace ServiceAbstraction.IClients
{
    public interface IClientContactService : IBaseService<int, ClientContactResponseDto, ClientContactCreateDto, ClientContactUpdateDto>
    {
    }
}
