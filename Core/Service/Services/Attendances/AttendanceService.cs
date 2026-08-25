using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;

using Service.Specifications;
using Shared.Dto.Attendances;
using ServiceAbstraction.IAttendances;

namespace Service.Services.Attendances
{
    public class AttendanceService : BaseService<Attendance, int, AttendanceResponseDto, AttendanceCreateDto, AttendanceUpdateDto>, IAttendanceService
    {
        public AttendanceService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AttendanceCreateDto> createValidator, IValidator<AttendanceUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }

        public async Task CheckInAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            var attendance = new Attendance
            {
                EmployeeId = employeeId,
                Date = DateTime.UtcNow.Date,
                CheckIn = DateTime.UtcNow.TimeOfDay,
                IsAbsent = false
            };
            await Repository.AddAsync(attendance, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task CheckOutAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            var today = DateTime.UtcNow.Date;
            var spec = new AttendanceSpecification(employeeId, today);
            var attendance = await Repository.FirstOrDefaultAsync(spec, cancellationToken);
            if (attendance == null)
            {
                attendance = new Attendance
                {
                    EmployeeId = employeeId,
                    Date = today,
                    CheckIn = TimeSpan.FromHours(9), // assume 9 AM check-in
                    IsAbsent = false
                };
                await Repository.AddAsync(attendance, cancellationToken);
            }
            attendance.CheckOut = DateTime.UtcNow.TimeOfDay;
            Repository.Update(attendance);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task ImportFingerprintAsync(CancellationToken cancellationToken = default)
        {
            // fingerprint import simulation
            return Task.CompletedTask;
        }

        public async Task<int> CalculateLateAsync(int employeeId, DateTime date, CancellationToken cancellationToken = default)
        {
            var spec = new AttendanceSpecification(employeeId, date);
            var attendance = await Repository.FirstOrDefaultAsync(spec, cancellationToken);
            if (attendance == null || !attendance.CheckIn.HasValue) return 0;

            var workStart = TimeSpan.FromHours(9); // 9:00 AM
            if (attendance.CheckIn.Value > workStart)
            {
                var diff = attendance.CheckIn.Value - workStart;
                attendance.LateMinutes = (int)diff.TotalMinutes;
                Repository.Update(attendance);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
                return attendance.LateMinutes;
            }
            return 0;
        }
    }
}
