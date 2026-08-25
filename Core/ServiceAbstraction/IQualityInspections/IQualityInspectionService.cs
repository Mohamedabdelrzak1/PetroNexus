using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.NonConformanceReports;

namespace ServiceAbstraction.IQualityInspections
{
    public interface IQualityInspectionService : IBaseService<int, QualityInspectionResponseDto, QualityInspectionCreateDto, QualityInspectionUpdateDto>
    {
    }
}
