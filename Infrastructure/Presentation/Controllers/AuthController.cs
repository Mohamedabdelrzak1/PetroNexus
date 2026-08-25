using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.Auth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<UserResultDto>>> Login([FromBody] LoginDto loginDto)
        {
            var result = await serviceManager.AuthService.LoginAsync(loginDto);
            return this.OkResponse(result, "Login successful.");
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult<ApiResponse>> ChangePassword(ChangePasswordDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return this.UnauthorizedResponse();

            await serviceManager.AuthService.ChangePasswordAsync(userId, dto);

            return this.OkResponse("Password changed successfully.");
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<ActionResult<ApiResponse>> ForgotPassword(ForgetPasswordDto dto)
        {
            await serviceManager.AuthService.ForgotPasswordAsync(dto);

            return this.OkResponse("Check your email — if it is registered, you will receive a password reset link shortly.");
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult<ApiResponse>> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            await serviceManager.AuthService.ResetPasswordAsync(dto);

            return this.OkResponse("Password reset successfully.");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<ApiResponse>> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await serviceManager.AuthService.LogoutAsync(userId!);

            return this.OkResponse("Logged out successfully.");
        }
    }
}