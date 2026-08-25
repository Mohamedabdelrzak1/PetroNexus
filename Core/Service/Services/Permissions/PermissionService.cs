using ServiceAbstraction.IPermissions;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.RolePermissions;

namespace Service.Services.Permissions
{
    public class PermissionService : BaseService<Permission, int, PermissionResponseDto, PermissionCreateDto, PermissionUpdateDto>, IPermissionService
    {
        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<PermissionCreateDto> createValidator, IValidator<PermissionUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
