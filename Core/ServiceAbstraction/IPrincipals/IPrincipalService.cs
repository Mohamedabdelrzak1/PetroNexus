using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Principals;

namespace ServiceAbstraction.IPrincipals
{
    public interface IPrincipalService : IBaseService<int, PrincipalResponseDto, PrincipalCreateDto, PrincipalUpdateDto>
    {
    }
}
