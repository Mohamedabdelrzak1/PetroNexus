using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IQualityInspections;
using Shared.Dto.NonConformanceReports;


namespace Service.Services.QualityInspections
{
    public class QualityInspectionService : BaseService<QualityInspection, int, QualityInspectionResponseDto, QualityInspectionCreateDto, QualityInspectionUpdateDto>, IQualityInspectionService
    {
        public QualityInspectionService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<QualityInspectionCreateDto> createValidator, IValidator<QualityInspectionUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
