using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.ITenders;
using Shared.Dto.Tenders;

namespace Service.Services.Tenders
{
    public class TenderLeadService : BaseService<TenderLead, int, TenderLeadResponseDto, TenderLeadCreateDto, TenderLeadUpdateDto>, ITenderLeadService
    {
        public TenderLeadService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<TenderLeadCreateDto> createValidator, IValidator<TenderLeadUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }

        /// <summary>
        /// Converts a TenderLead into a formal Tender.
        /// Creates a new Tender with the available lead data and marks the lead as converted.
        /// </summary>
        public async Task<TenderResponseDto> ConvertToTenderAsync(int id, CancellationToken cancellationToken = default)
        {
            var lead = await Repository.GetByIdAsync(id, cancellationToken);
            if (lead is null)
                throw new KeyNotFoundException($"TenderLead #{id} not found.");

            if (lead.IsConverted)
                throw new InvalidOperationException($"TenderLead #{id} is already converted to Tender #{lead.ConvertedTenderId}.");

            var tender = new Tender
            {
                Title = lead.Title,
                ReferenceNumber = null,
                ClientId = 0, // Placeholder — client must be assigned after conversion
                AnnouncementDate = lead.DiscoveredAt,
                SubmissionDeadline = lead.SubmissionDeadline ?? lead.DiscoveredAt.AddDays(30),
                Status = TenderStatus.New,
                EstimatedValue = null,
                Currency = CurrencyType.EGP,
                Notes = $"Converted from TenderLead #{lead.Id} — Source: {lead.SourcePortalName}",
                Source = TenderSource.Manual,
                TenderLeadId = lead.Id,
                CreatedAt = DateTime.UtcNow
            };

            await UnitOfWork.Repository<Tender, int>().AddAsync(tender, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            // Mark the lead as converted
            lead.IsConverted = true;
            lead.ConvertedTenderId = tender.Id;
            Repository.Update(lead);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return Mapper.Map<TenderResponseDto>(tender);
        }
    }
}