using ServiceAbstraction.IJournalEntries;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.JournalEntries;

namespace Service.Services.JournalEntries
{
    public class JournalEntryService : BaseService<JournalEntry, int, JournalEntryResponseDto, JournalEntryCreateDto, JournalEntryUpdateDto>, IJournalEntryService
    {
        public JournalEntryService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<JournalEntryCreateDto> createValidator, IValidator<JournalEntryUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
