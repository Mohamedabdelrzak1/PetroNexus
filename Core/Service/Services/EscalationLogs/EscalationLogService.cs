using System;
using System.Threading;
using System.Threading.Tasks;
using ServiceAbstraction.IEscalationLogs;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.EscalationLogs;

namespace Service.Services.EscalationLogs
{
    public class EscalationLogService : BaseService<EscalationLog, int, EscalationLogResponseDto, EscalationLogCreateDto, EscalationLogUpdateDto>, IEscalationLogService
    {
        public EscalationLogService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<EscalationLogCreateDto> createValidator, IValidator<EscalationLogUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }

        /// <summary>Acknowledges an escalation log.</summary>
        public async Task AcknowledgeAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                throw new KeyNotFoundException($"EscalationLog #{id} not found.");

            entity.WasAcknowledged = true;
            Repository.Update(entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}