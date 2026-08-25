using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.JournalEntries;


namespace ServiceAbstraction.IJournalEntries
{
    public interface IJournalEntryLineService : IBaseService<int, JournalEntryLineResponseDto, JournalEntryLineCreateDto, JournalEntryLineUpdateDto>
    {
    }
}
