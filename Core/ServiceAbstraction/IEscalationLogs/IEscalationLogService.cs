using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.EscalationLogs;

namespace ServiceAbstraction.IEscalationLogs
{
    public interface IEscalationLogService : IBaseService<int, EscalationLogResponseDto, EscalationLogCreateDto, EscalationLogUpdateDto>
    {
        /// <summary>Acknowledges an escalation log.</summary>
        Task AcknowledgeAsync(int id, CancellationToken cancellationToken = default);
    }
}
