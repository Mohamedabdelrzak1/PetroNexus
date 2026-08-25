using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.ILettersOfCredit;
using Shared.Dto.LettersOfCredit;

namespace Service.Services.LettersOfCredit
{
    public class LetterOfCreditService : BaseService<LetterOfCredit, int, LetterOfCreditResponseDto, LetterOfCreditCreateDto, LetterOfCreditUpdateDto>, ILetterOfCreditService
    {
        public LetterOfCreditService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<LetterOfCreditCreateDto> createValidator, IValidator<LetterOfCreditUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
