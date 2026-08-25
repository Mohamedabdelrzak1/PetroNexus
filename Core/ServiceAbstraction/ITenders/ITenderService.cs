using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Tenders;

namespace ServiceAbstraction.ITenders
{
    public interface ITenderService : IBaseService<int, TenderResponseDto, TenderCreateDto, TenderUpdateDto>
    {
    }
}
