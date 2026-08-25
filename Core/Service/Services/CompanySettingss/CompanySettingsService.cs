using ServiceAbstraction.ICompanySettings;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.CompanySettings;

namespace Service.Services.CompanySettingss
{
    public class CompanySettingsService : BaseService<CompanySettings, int, CompanySettingsResponseDto, CompanySettingsCreateDto, CompanySettingsUpdateDto>, ICompanySettingsService
    {
        public CompanySettingsService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CompanySettingsCreateDto> createValidator, IValidator<CompanySettingsUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
