using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Principals; // PrincipalContactResponseDto is in PrincipalDtos.cs namespaces or similar

namespace ServiceAbstraction.IPrincipals
{
    public interface IPrincipalContactService : IBaseService<int, PrincipalContactResponseDto, PrincipalContactCreateDto, PrincipalContactUpdateDto>
    {
    }
}
