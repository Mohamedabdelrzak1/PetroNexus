using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Tenders;

namespace ServiceAbstraction.ITenders
{
    public interface ITenderLeadService : IBaseService<int, TenderLeadResponseDto, TenderLeadCreateDto, TenderLeadUpdateDto>
    {
        /// <summary>Converts a TenderLead into a formal Tender.</summary>
        Task<TenderResponseDto> ConvertToTenderAsync(int id, CancellationToken cancellationToken = default);
    }
}
