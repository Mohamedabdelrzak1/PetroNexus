using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Tenders;

namespace ServiceAbstraction.ITenders
{
    public interface ITenderItemService : IBaseService<int, TenderItemResponseDto, TenderItemCreateDto, TenderItemUpdateDto>
    {
    }
}
