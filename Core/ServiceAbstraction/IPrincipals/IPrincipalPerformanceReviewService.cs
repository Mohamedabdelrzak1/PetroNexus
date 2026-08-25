using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Principals;

namespace ServiceAbstraction.IPrincipals
{
    public interface IPrincipalPerformanceReviewService : IBaseService<int, PrincipalPerformanceReviewResponseDto, PrincipalPerformanceReviewCreateDto, PrincipalPerformanceReviewUpdateDto>
    {
    }
}
