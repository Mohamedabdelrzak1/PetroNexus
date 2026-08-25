using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Shared.Dto.DocumentRecords
{
    public class DocumentRecordCreateDto
    {
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DocumentCategory Category { get; set; }
        public string RelatedEntityType { get; set; } = null!;
        public int RelatedEntityId { get; set; }
        public string? UploadedByUserId { get; set; }
    }

    public class DocumentRecordUpdateDto
    {
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DocumentCategory Category { get; set; }
        public string RelatedEntityType { get; set; } = null!;
        public int RelatedEntityId { get; set; }
        public string? UploadedByUserId { get; set; }
    }

    public class DocumentRecordResponseDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DocumentCategory Category { get; set; }
        public string RelatedEntityType { get; set; } = null!;
        public int RelatedEntityId { get; set; }
        public DateTime UploadedAt { get; set; }
        public string? UploadedByUserId { get; set; }
    }

    public class DocumentRecordDetailsDto : DocumentRecordResponseDto
    {
        public List<Shared.Dto.DocumentSignatures.DocumentSignatureResponseDto> Signatures { get; set; } = new();
    }
}
