using ServiceAbstraction.IPayments;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.Payments;

namespace Service.Services.Payments
{
    public class PaymentService : BaseService<Payment, int, PaymentResponseDto, PaymentCreateDto, PaymentUpdateDto>, IPaymentService
    {
        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<PaymentCreateDto> createValidator, IValidator<PaymentUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
