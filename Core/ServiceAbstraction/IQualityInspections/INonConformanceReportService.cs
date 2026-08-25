using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.NonConformanceReports;

namespace ServiceAbstraction.IQualityInspections
{
    public interface INonConformanceReportService : IBaseService<int, NonConformanceReportResponseDto, NonConformanceReportCreateDto, NonConformanceReportUpdateDto>
    {
    }
}
