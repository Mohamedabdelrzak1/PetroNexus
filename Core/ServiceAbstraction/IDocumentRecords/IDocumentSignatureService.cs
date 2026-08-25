using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.DocumentSignatures;

namespace ServiceAbstraction.IDocumentRecords
{
    public interface IDocumentSignatureService : IBaseService<int, DocumentSignatureResponseDto, DocumentSignatureCreateDto, DocumentSignatureUpdateDto>
    {
        /// <summary>Signs a document signature (computes SHA256 hash).</summary>
        Task SignAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>Rejects a document signature with a reason.</summary>
        Task RejectAsync(int id, string rejectionReason, CancellationToken cancellationToken = default);
    }
}
