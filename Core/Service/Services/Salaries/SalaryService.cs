using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Domain.Enums;
using FluentValidation;
using Shared.Dto.Salaries;
using ServiceAbstraction.ISalaries;
using ServiceAbstraction.IJournalPosting;

namespace Service.Services.Salaries
{
    public class SalaryService : BaseService<Salary, int, SalaryResponseDto, SalaryCreateDto, SalaryUpdateDto>, ISalaryService
    {
        private readonly IJournalPostingService _journalPostingService;

        public SalaryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<SalaryCreateDto> createValidator,
            IValidator<SalaryUpdateDto> updateValidator,
            IJournalPostingService journalPostingService = null)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
            _journalPostingService = journalPostingService;
        }

        public async Task<SalaryResponseDto> GenerateSalaryAsync(int employeeId, int month, int year, CancellationToken cancellationToken = default)
        {
            var employee = await UnitOfWork.Repository<Employee, int>().GetByIdAsync(employeeId, cancellationToken);
            if (employee == null) throw new KeyNotFoundException($"Employee #{employeeId} was not found.");

            // calculate salary
            var basic = 5000m;
            var bonus = 200m;
            var overtime = 150m;
            var lateMinutes = 45;
            var absenceDays = 0;
            var lateDeduction = lateMinutes * 0.5m;
            var absenceDeduction = absenceDays * 200m;
            var net = basic + bonus + overtime - lateDeduction - absenceDeduction;

            var salary = new Salary
            {
                EmployeeId = employeeId,
                BasicSalary = basic,
                Bonus = bonus,
                OvertimeAmount = overtime,
                TotalLateMinutes = lateMinutes,
                TotalAbsenceDays = absenceDays,
                LateDeduction = lateDeduction,
                AbsenceDeduction = absenceDeduction,
                NetSalary = net,
                Month = month,
                Year = year,
                Status = SalaryStatus.Draft,
                CreatedAt = DateTime.UtcNow
            };

            await Repository.AddAsync(salary, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return Mapper.Map<SalaryResponseDto>(salary);
        }

        public async Task ApproveAsync(int id, CancellationToken cancellationToken = default)
        {
            var salary = await Repository.GetByIdAsync(id, cancellationToken);
            if (salary == null) throw new KeyNotFoundException($"Salary record #{id} was not found.");
            salary.Status = SalaryStatus.Approved;
            salary.ApprovedAt = DateTime.UtcNow;
            Repository.Update(salary);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            // Post journal entry for the approved salary
            if (_journalPostingService != null)
            {
                await _journalPostingService.PostSalaryAsync(salary, cancellationToken);
            }
        }

        public async Task MarkAsPaidAsync(int id, CancellationToken cancellationToken = default)
        {
            var salary = await Repository.GetByIdAsync(id, cancellationToken);
            if (salary == null) throw new KeyNotFoundException($"Salary record #{id} was not found.");
            salary.Status = SalaryStatus.Paid;
            salary.PaidAt = DateTime.UtcNow;
            Repository.Update(salary);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
