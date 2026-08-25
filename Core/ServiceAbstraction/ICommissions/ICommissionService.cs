using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Commissions;

namespace ServiceAbstraction.ICommissions
{
    public interface ICommissionService : IBaseService<int, CommissionResponseDto, CommissionCreateDto, CommissionUpdateDto>
    {
    }
}
