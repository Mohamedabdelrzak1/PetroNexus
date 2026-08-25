using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.LeaveRequests;

namespace ServiceAbstraction.ILeaveRequests
{
    public interface ILeaveRequestService : IBaseService<int, LeaveRequestResponseDto, LeaveRequestCreateDto, LeaveRequestUpdateDto>
    {
        Task ApproveAsync(int id, CancellationToken cancellationToken = default);
        Task RejectAsync(int id, CancellationToken cancellationToken = default);
    }
}
