using ServiceAbstraction.IPermissions;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.RolePermissions;

namespace Service.Services.Permissions
{
    public class RolePermissionService : BaseService<RolePermission, int, RolePermissionResponseDto, RolePermissionCreateDto, RolePermissionUpdateDto>, IRolePermissionService
    {
        public RolePermissionService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RolePermissionCreateDto> createValidator, IValidator<RolePermissionUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
