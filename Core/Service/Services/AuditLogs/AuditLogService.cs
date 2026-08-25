
using ServiceAbstraction.IAuditLogs;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.AuditLogs;

namespace Service.Services.AuditLogs
{
    public class AuditLogService : BaseService<AuditLog, int, AuditLogResponseDto, AuditLogCreateDto, AuditLogCreateDto>, IAuditLogService
    {
        public AuditLogService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AuditLogCreateDto>? createValidator, IValidator<AuditLogCreateDto>? updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
