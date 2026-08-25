using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using ServiceAbstraction.IAuthServices;
using Shared.Common;
using Shared.Dto;
using Shared.Dto.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IMailingService _mailService;


        public AuthService(
            UserManager<AppUser> userManager,
            IOptions<JwtOptions> options,
            IMailingService mailService)
        {
            _userManager = userManager;
            _jwtOptions = options.Value;
            _mailService = mailService;
        }
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                throw new UnAuthorizedException("البريد الإلكتروني أو كلمة المرور غير صحيحة");

            // لو الحساب غير مفعل
            if (!user.IsActive)
                throw new UnAuthorizedException(
                    "تم إيقاف هذا الحساب. برجاء التواصل مع الإدارة لتفعيل الحساب");


            // lockout
            if (await _userManager.IsLockedOutAsync(user))
                throw new UnAuthorizedException("Account locked. Try again later.");

            var validPassword =
                await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!validPassword)
            {
                await _userManager.AccessFailedAsync(user);
                throw new UnAuthorizedException("Invalid email or password");
            }

            // reset failed attempts
            await _userManager.ResetAccessFailedCountAsync(user);

            // =========================
            // Presence update
            // =========================
            var now = DateTime.UtcNow;

            user.IsOnline = true;
            user.LastLogin = now;
            user.LastSeen = now;

            var update = await _userManager.UpdateAsync(user);

            if (!update.Succeeded)
                throw new Exception(string.Join(", ", update.Errors.Select(e => e.Description)));

            // =========================
            // JWT + Roles
            // =========================
            var token = await GenerateJwtTokenAsync(user);

            return new UserResultDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = token,

                // presence
                IsOnline = true,
                LastLogin = now,
                LastSeen = now
            };
        }


        public async Task LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new Exception("User not found");

            user.IsOnline = false;
            user.LastSeen = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> ForgotPasswordAsync(ForgetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            // مهم أوي: نرجع true حتى لو المستخدم مش موجود
            // عشان ما نكشفش الإيميلات في السيستم
            if (user == null)
                return true;

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Uri.EscapeDataString(resetToken);

            var resetUrl =
                $"https://real-estate-accounting-app-two.vercel.app/reset-password" +
                $"?email={Uri.EscapeDataString(user.Email)}&token={encodedToken}";

            var htmlBody = $@"
<p>Hello {user.DisplayName ?? user.Email},</p>
<p>You requested to reset your password.</p>
<p>
<a href=""{resetUrl}"" style=""padding:10px 18px;background:#2563eb;color:#fff;text-decoration:none;border-radius:6px;"">
Reset Password
</a>
</p>
<p style=""word-break:break-all"">{resetUrl}</p>";

            await _mailService.SendEmailAsync(new Email
            {
                To = user.Email,
                Subject = "Reset Password",
                Body = htmlBody
            });

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmPassword)
                throw new BadRequestException("Passwords do not match");

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new NotFoundExceptions("User not found");

            var token = Uri.UnescapeDataString(dto.Token);

            var result = await _userManager.ResetPasswordAsync(
                user,
                token,
                dto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(errors);
            }

            return true;
        }

        public async Task ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmPassword)
                throw new BadRequestException("Passwords do not match");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new NotFoundExceptions("User not found");

            var result = await _userManager.ChangePasswordAsync(
                user,
                dto.CurrentPassword,
                dto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new BadRequestException(errors);
            }

            // تحديث آخر نشاط
            user.LastSeen = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
        }


        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        // ============================
        // JWT TOKEN GENERATION (FINAL)
        // ============================
        private async Task<string> GenerateJwtTokenAsync(AppUser user)
        {
            var now = DateTime.UtcNow;

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(ClaimTypes.Name, user.UserName ?? ""),
        new Claim(ClaimTypes.Email, user.Email ?? ""),
        new Claim(ClaimTypes.NameIdentifier, user.Id),

        new Claim("userNumericId", user.NumericId.ToString()),

        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat,
            new DateTimeOffset(now).ToUnixTimeSeconds().ToString(),
            ClaimValueTypes.Integer64)
    };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expires = now.AddDays(_jwtOptions.DurationInDays);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
