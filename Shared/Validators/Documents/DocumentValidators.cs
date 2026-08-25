using FluentValidation;
using Shared.Dto.DocumentRecords;
using Shared.Dto.DocumentSignatures;

namespace Shared.Validators.Documents
{
    public class CreateDocumentRecordDtoValidator : AbstractValidator<DocumentRecordCreateDto>
    {
        public CreateDocumentRecordDtoValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.FilePath).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Category).IsInEnum();
        }
    }

    public class UpdateDocumentRecordDtoValidator : AbstractValidator<DocumentRecordUpdateDto>
    {
        public UpdateDocumentRecordDtoValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.FilePath).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Category).IsInEnum();
        }
    }

    public class CreateDocumentSignatureDtoValidator : AbstractValidator<DocumentSignatureCreateDto>
    {
        public CreateDocumentSignatureDtoValidator()
        {
            RuleFor(x => x.DocumentRecordId).GreaterThan(0);
            RuleFor(x => x.SignerUserId).NotEmpty();
        }
    }

    public class UpdateDocumentSignatureDtoValidator : AbstractValidator<DocumentSignatureUpdateDto>
    {
        public UpdateDocumentSignatureDtoValidator()
        {
            RuleFor(x => x.DocumentRecordId).GreaterThan(0);
            RuleFor(x => x.SignerUserId).NotEmpty();
        }
    }
}