using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Auth;


namespace ServiceAbstraction.IAuthServices
{
    public interface IRefreshTokenService : IBaseService<int, RefreshTokenResponseDto, RefreshTokenCreateDto, RefreshTokenUpdateDto>
    {
    }
}
