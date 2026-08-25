using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Accounts;

namespace ServiceAbstraction.IAccounts
{
    public interface IAccountService : IBaseService<int, AccountResponseDto, AccountCreateDto, AccountUpdateDto>
    {
    }
}
