using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Clients;

namespace ServiceAbstraction.IClients
{
    public interface IClientPortalUserService : IBaseService<int, ClientPortalUserResponseDto, ClientPortalUserCreateDto, ClientPortalUserUpdateDto>
    {
    }
}
