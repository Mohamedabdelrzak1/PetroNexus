using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.CompanySettings;

namespace ServiceAbstraction.ICompanySettings
{
    public interface ICompanySettingsService : IBaseService<int, CompanySettingsResponseDto, CompanySettingsCreateDto, CompanySettingsUpdateDto>
    {
    }
}
