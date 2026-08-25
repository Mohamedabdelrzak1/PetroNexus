using ServiceAbstraction.IJournalEntries;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.JournalEntries;

namespace Service.Services.JournalEntries
{
    public class JournalEntryLineService : BaseService<JournalEntryLine, int, JournalEntryLineResponseDto, JournalEntryLineCreateDto, JournalEntryLineUpdateDto>, IJournalEntryLineService
    {
        public JournalEntryLineService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<JournalEntryLineCreateDto> createValidator, IValidator<JournalEntryLineUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
