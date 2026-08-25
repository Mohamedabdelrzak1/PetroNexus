using Shared.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.IAuthServices
{
    public interface IAuthService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);

        Task ChangePasswordAsync(string userId, ChangePasswordDto dto);

        // Check Email Exists
        Task<bool> CheckEmailExistsAsync(string email);
        Task LogoutAsync(string userId);
        Task<bool> ForgotPasswordAsync(ForgetPasswordDto dto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);





    }
}
