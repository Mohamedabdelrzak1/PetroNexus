using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.ExchangeRates;

namespace ServiceAbstraction.IExchangeRates
{
    public interface IExchangeRateService : IBaseService<int, ExchangeRateResponseDto, ExchangeRateCreateDto, ExchangeRateUpdateDto>
    {
    }
}
