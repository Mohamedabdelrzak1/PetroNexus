using ServiceAbstraction.IDocumentRecords;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.DocumentRecords;

namespace Service.Services.DocumentRecords
{
    public class DocumentRecordService : BaseService<DocumentRecord, int, DocumentRecordResponseDto, DocumentRecordCreateDto, DocumentRecordUpdateDto>, IDocumentRecordService
    {
        public DocumentRecordService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<DocumentRecordCreateDto> createValidator, IValidator<DocumentRecordUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
