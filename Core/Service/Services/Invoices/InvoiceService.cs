using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServiceAbstraction.IInvoices;
using ServiceAbstraction.IJournalPosting;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Dto.Invoices;

namespace Service.Services.Invoices
{
    public class InvoiceService : BaseService<Invoice, int, InvoiceResponseDto, InvoiceCreateDto, InvoiceUpdateDto>, IInvoiceService
    {
        private readonly IJournalPostingService _journalPostingService;

        public InvoiceService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<InvoiceCreateDto> createValidator,
            IValidator<InvoiceUpdateDto> updateValidator,
            IJournalPostingService journalPostingService = null)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
            _journalPostingService = journalPostingService;
        }

        public async Task<InvoiceResponseDto> GenerateInvoiceAsync(int tenderId, CancellationToken cancellationToken = default)
        {
            var tender = await UnitOfWork.Repository<Tender, int>().GetByIdAsync(tenderId, cancellationToken);
            if (tender == null) throw new KeyNotFoundException($"Tender #{tenderId} not found.");

            var invoice = new Invoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{tenderId:D4}",
                TenderId = tenderId,
                ClientId = tender.ClientId,
                IssueDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                Currency = tender.Currency,
                Status = InvoiceStatus.Draft,
                RetentionPercentage = 5,
                CreatedAt = DateTime.UtcNow
            };

            await Repository.AddAsync(invoice, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return Mapper.Map<InvoiceResponseDto>(invoice);
        }

        public async Task ApproveAsync(int id, CancellationToken cancellationToken = default)
        {
            var invoice = await Repository.GetByIdAsync(id, cancellationToken);
            if (invoice == null) throw new KeyNotFoundException($"Invoice #{id} not found.");
            invoice.Status = InvoiceStatus.Approved;
            Repository.Update(invoice);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            // Post journal entry for the approved invoice
            if (_journalPostingService != null)
            {
                // Reload with items to get TotalAmount
                var invoiceWithItems = await UnitOfWork.Repository<Invoice, int>()
                    .GetFirstOrDefaultAsync(i => i.Id == id, q => q.Include(i => i.Items), false, cancellationToken);
                if (invoiceWithItems != null)
                {
                    await _journalPostingService.PostInvoiceAsync(invoiceWithItems, cancellationToken);
                }
            }
        }

        public async Task RecordPaymentAsync(int invoiceId, decimal amount, string method, string referenceNumber, CancellationToken cancellationToken = default)
        {
            var invoice = await Repository.GetByIdAsync(invoiceId, cancellationToken);
            if (invoice == null) throw new KeyNotFoundException($"Invoice #{invoiceId} not found.");

            var payment = new Payment
            {
                InvoiceId = invoiceId,
                Amount = amount,
                Method = method,
                ReferenceNumber = referenceNumber,
                PaymentDate = DateTime.UtcNow
            };

            await UnitOfWork.Repository<Payment, int>().AddAsync(payment, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            // Post journal entry for the payment
            if (_journalPostingService != null)
            {
                await _journalPostingService.PostPaymentAsync(payment, invoice, cancellationToken);
            }

            // Update invoice status based on payment
            var invoiceWithPayments = await UnitOfWork.Repository<Invoice, int>()
                .GetFirstOrDefaultAsync(i => i.Id == invoiceId, q => q.Include(i => i.Payments), false, cancellationToken);
            if (invoiceWithPayments != null)
            {
                if (invoiceWithPayments.RemainingBalance <= 0)
                    invoice.Status = InvoiceStatus.Paid;
                else
                    invoice.Status = InvoiceStatus.PartiallyPaid;

                Repository.Update(invoice);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task CancelAsync(int id, CancellationToken cancellationToken = default)
        {
            var invoice = await Repository.GetByIdAsync(id, cancellationToken);
            if (invoice == null) throw new KeyNotFoundException($"Invoice #{id} not found.");
            invoice.Status = InvoiceStatus.Cancelled;
            Repository.Update(invoice);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
