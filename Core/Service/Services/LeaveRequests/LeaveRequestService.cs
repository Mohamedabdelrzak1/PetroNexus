using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.ILeaveRequests;
using Shared.Dto.LeaveRequests;

namespace Service.Services.LeaveRequests
{
    public class LeaveRequestService : BaseService<LeaveRequest, int, LeaveRequestResponseDto, LeaveRequestCreateDto, LeaveRequestUpdateDto>, ILeaveRequestService
    {
        public LeaveRequestService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<LeaveRequestCreateDto> createValidator, IValidator<LeaveRequestUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }

        public async Task ApproveAsync(int id, CancellationToken cancellationToken = default)
        {
            var leave = await Repository.GetByIdAsync(id, cancellationToken);
            if (leave == null) throw new KeyNotFoundException($"Leave request #{id} was not found.");
            leave.IsApproved = true;
            leave.ApprovedAt = DateTime.UtcNow;
            Repository.Update(leave);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task RejectAsync(int id, CancellationToken cancellationToken = default)
        {
            var leave = await Repository.GetByIdAsync(id, cancellationToken);
            if (leave == null) throw new KeyNotFoundException($"Leave request #{id} was not found.");
            leave.IsApproved = false;
            leave.ApprovedAt = DateTime.UtcNow;
            Repository.Update(leave);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
