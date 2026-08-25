using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.DocumentRecords;

namespace ServiceAbstraction.IDocumentRecords
{
    public interface IDocumentRecordService : IBaseService<int, DocumentRecordResponseDto, DocumentRecordCreateDto, DocumentRecordUpdateDto>
    {
    }
}
