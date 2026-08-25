using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.LettersOfCredit;

namespace ServiceAbstraction.ILettersOfCredit
{
    public interface ILetterOfCreditService : IBaseService<int, LetterOfCreditResponseDto, LetterOfCreditCreateDto, LetterOfCreditUpdateDto>
    {
    }
}
