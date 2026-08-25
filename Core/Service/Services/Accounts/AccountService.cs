using ServiceAbstraction.IAccounts;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Shared.Dto.Accounts;

namespace Service.Services.Accounts
{
    public class AccountService : BaseService<Account, int, AccountResponseDto, AccountCreateDto, AccountUpdateDto>, IAccountService
    {
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AccountCreateDto> createValidator, IValidator<AccountUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }
    }
}
