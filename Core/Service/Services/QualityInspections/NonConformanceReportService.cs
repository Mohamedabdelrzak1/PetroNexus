using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IQualityInspections;
using Shared.Dto.NonConformanceReports;

namespace Service.Services.QualityInspections
{
    public class NonConformanceReportService : BaseService<NonConformanceReport, int, NonConformanceReportResponseDto, NonConformanceReportCreateDto, NonConformanceReportUpdateDto>, INonConformanceReportService
    {
        public NonConformanceReportService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<NonConformanceReportCreateDto> createValidator, IValidator<NonConformanceReportUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
