using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.ITenders;
using Shared.Dto.Tenders;

namespace Service.Services.Tenders
{
    public class TenderDocumentAnalysisService : BaseService<TenderDocumentAnalysis, int, TenderDocumentAnalysisResponseDto, TenderDocumentAnalysisCreateDto, TenderDocumentAnalysisUpdateDto>, ITenderDocumentAnalysisService
    {
        public TenderDocumentAnalysisService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<TenderDocumentAnalysisCreateDto> createValidator, IValidator<TenderDocumentAnalysisUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
