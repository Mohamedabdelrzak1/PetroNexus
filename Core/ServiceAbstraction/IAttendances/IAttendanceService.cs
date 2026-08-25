using System;
using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Attendances;

namespace ServiceAbstraction.IAttendances
{
    public interface IAttendanceService : IBaseService<int, AttendanceResponseDto, AttendanceCreateDto, AttendanceUpdateDto>
    {
        Task CheckInAsync(int employeeId, CancellationToken cancellationToken = default);
        Task CheckOutAsync(int employeeId, CancellationToken cancellationToken = default);
        Task ImportFingerprintAsync(CancellationToken cancellationToken = default);
        Task<int> CalculateLateAsync(int employeeId, DateTime date, CancellationToken cancellationToken = default);
    }
}
