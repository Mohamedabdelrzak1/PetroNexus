using System;
using Domain.Enums;

namespace Shared.Dto.Accounts
{
    public class AccountCreateDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public AccountType Type { get; set; }
        public int? ParentAccountId { get; set; }
    }

    public class AccountUpdateDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public AccountType Type { get; set; }
        public int? ParentAccountId { get; set; }
    }

    public class AccountResponseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public AccountType Type { get; set; }
        public int? ParentAccountId { get; set; }
        public string? ParentAccountName { get; set; }
    }
}
