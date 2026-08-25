using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.RolePermissions;

namespace ServiceAbstraction.IPermissions
{
    public interface IRolePermissionService : IBaseService<int, RolePermissionResponseDto, RolePermissionCreateDto, RolePermissionUpdateDto>
    {
    }
}
