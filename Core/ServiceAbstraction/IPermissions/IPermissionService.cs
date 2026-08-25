using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.RolePermissions;

namespace ServiceAbstraction.IPermissions
{
    public interface IPermissionService : IBaseService<int, PermissionResponseDto, PermissionCreateDto, PermissionUpdateDto>
    {
    }
}
