using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IAuthServices;
using Shared.Dto.Auth;

namespace Service.Services.RefreshTokens
{
    public class RefreshTokenService : BaseService<RefreshToken, int, RefreshTokenResponseDto, RefreshTokenCreateDto, RefreshTokenUpdateDto>, IRefreshTokenService
    {
        public RefreshTokenService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RefreshTokenCreateDto>? createValidator, IValidator<RefreshTokenUpdateDto>? updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
