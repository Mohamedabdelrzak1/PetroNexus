using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Principals;

namespace ServiceAbstraction.IPrincipals
{
    public interface IPrincipalProductService : IBaseService<int, PrincipalProductResponseDto, PrincipalProductCreateDto, PrincipalProductUpdateDto>
    {
    }
}
