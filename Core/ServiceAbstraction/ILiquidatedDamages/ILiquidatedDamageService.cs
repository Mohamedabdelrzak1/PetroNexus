using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.LiquidatedDamages;

namespace ServiceAbstraction.ILiquidatedDamages
{
    public interface ILiquidatedDamageService : IBaseService<int, LiquidatedDamageResponseDto, LiquidatedDamageCreateDto, LiquidatedDamageUpdateDto>
    {
    }
}
