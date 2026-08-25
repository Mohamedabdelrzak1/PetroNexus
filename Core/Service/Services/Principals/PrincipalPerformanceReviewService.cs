using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IPrincipals;
using Shared.Dto.Principals;

namespace Service.Services.Principals
{
    public class PrincipalPerformanceReviewService : BaseService<PrincipalPerformanceReview, int, PrincipalPerformanceReviewResponseDto, PrincipalPerformanceReviewCreateDto, PrincipalPerformanceReviewUpdateDto>, IPrincipalPerformanceReviewService
    {
        public PrincipalPerformanceReviewService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<PrincipalPerformanceReviewCreateDto> createValidator, IValidator<PrincipalPerformanceReviewUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
