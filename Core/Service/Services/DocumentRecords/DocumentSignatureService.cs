using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceAbstraction.IDocumentRecords;
using AutoMapper;
using Domain.Contracts;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Dto.DocumentSignatures;

namespace Service.Services.DocumentRecords
{
    public class DocumentSignatureService : BaseService<DocumentSignature, int, DocumentSignatureResponseDto, DocumentSignatureCreateDto, DocumentSignatureUpdateDto>, IDocumentSignatureService
    {
        public DocumentSignatureService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<DocumentSignatureCreateDto> createValidator, IValidator<DocumentSignatureUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }

        /// <summary>
        /// Signs a document signature.
        /// Computes a SHA256 hash over the document content + signer + timestamp.
        /// </summary>
        public async Task SignAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                throw new KeyNotFoundException($"DocumentSignature #{id} not found.");

            if (entity.Status == SignatureStatus.Signed)
                throw new InvalidOperationException($"DocumentSignature #{id} is already signed.");

            // Load the document record to include its content in the hash
            var document = await UnitOfWork.Repository<DocumentRecord, int>()
                .GetFirstOrDefaultAsync(d => d.Id == entity.DocumentRecordId, null, false, cancellationToken);

            var now = DateTime.UtcNow;
            var hashInput = $"{document?.FileName ?? "document"}|{document?.FilePath ?? ""}|{entity.SignerUserId}|{now:O}";
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(hashInput));
            var hash = Convert.ToHexString(hashBytes);

            entity.Status = SignatureStatus.Signed;
            entity.SignedAt = now;
            entity.SignatureHash = hash;
            entity.RejectionReason = null;

            Repository.Update(entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        /// <summary>Rejects a document signature with a reason.</summary>
        public async Task RejectAsync(int id, string rejectionReason, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(rejectionReason))
                throw new ArgumentException("Rejection reason is required.", nameof(rejectionReason));

            var entity = await Repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                throw new KeyNotFoundException($"DocumentSignature #{id} not found.");

            entity.Status = SignatureStatus.Rejected;
            entity.RejectionReason = rejectionReason;
            entity.SignedAt = null;

            Repository.Update(entity);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}