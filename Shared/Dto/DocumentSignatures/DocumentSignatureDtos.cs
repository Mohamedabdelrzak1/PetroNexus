using System;
using Domain.Enums;

namespace Shared.Dto.DocumentSignatures
{
    public class DocumentSignatureCreateDto
    {
        public int DocumentRecordId { get; set; }
        public string SignerUserId { get; set; } = null!;
        public SignatureStatus Status { get; set; }
        public string? SignatureImagePath { get; set; }
        public string? SignatureHash { get; set; }
        public string? IpAddress { get; set; }
    }

    public class DocumentSignatureUpdateDto
    {
        public int DocumentRecordId { get; set; }
        public string SignerUserId { get; set; } = null!;
        public SignatureStatus Status { get; set; }
        public string? SignatureImagePath { get; set; }
        public string? SignatureHash { get; set; }
        public DateTime? SignedAt { get; set; }
        public string? IpAddress { get; set; }
    }

    public class DocumentSignatureResponseDto
    {
        public int Id { get; set; }
        public int DocumentRecordId { get; set; }
        public string SignerUserId { get; set; } = null!;
        public string SignerUserName { get; set; } = null!;
        public SignatureStatus Status { get; set; }
        public string? SignatureImagePath { get; set; }
        public string? SignatureHash { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? SignedAt { get; set; }
        public string? IpAddress { get; set; }
    }
}
