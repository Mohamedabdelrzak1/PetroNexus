using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.AuditLogs;

namespace ServiceAbstraction.IAuditLogs
{
    public interface IAuditLogService : IBaseService<int, AuditLogResponseDto, AuditLogCreateDto, AuditLogCreateDto>
    {
    }
}
