using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceAbstraction.IUser;
using Shared.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        // ✅ جلب كل المستخدمين بالترتيب حسب NumericId
        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userManager.Users
                .OrderBy(u => u.NumericId)
                .ToListAsync();

            var result = new List<UserResponseDto>();
            
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var dto = _mapper.Map<UserResponseDto>(user);

                dto.Role = roles;
                dto.RoleName = roles.FirstOrDefault() ?? string.Empty;
                dto.NumericId = user.NumericId;



                dto.IsOnline = CalculateOnline(user.LastSeen);
                dto.LastLogin = user.LastLogin;
                dto.LastSeen = user.LastSeen;

                // لو عندك Presence في DTO
                dto.Presence = GetPresenceText(user.LastSeen);

                result.Add(dto);
            }

            return result;
        }

        // ✅ جلب مستخدم برقم NumericId
        public async Task<UserResponseDto?> GetByIdAsync(int numericId)
        {
            var user = await GetUserByNumericIdAsync(numericId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var dto = _mapper.Map<UserResponseDto>(user);

            dto.Role = roles;
            dto.RoleName = roles.FirstOrDefault() ?? string.Empty;
            dto.NumericId = user.NumericId;

            dto.IsOnline = CalculateOnline(user.LastSeen);
            dto.LastLogin = user.LastLogin;
            dto.LastSeen = user.LastSeen;

            // لو عندك Presence في DTO
            dto.Presence = GetPresenceText(user.LastSeen);


            return dto;
        }
        private string GetPresenceText(DateTime? lastSeen)
        {
            if (lastSeen == null)
                return "غير متصل";

            var diff = DateTime.UtcNow - lastSeen.Value;

            if (diff.TotalMinutes < 2)
                return "متصل الآن";

            if (diff.TotalMinutes < 60)
                return $"آخر ظهور منذ {(int)diff.TotalMinutes} دقيقة";

            if (diff.TotalHours < 24)
                return $"آخر ظهور منذ {(int)diff.TotalHours} ساعة";

            return $"آخر ظهور {lastSeen:yyyy-MM-dd HH:mm}";
        }
        private bool CalculateOnline(DateTime? lastSeen)
        {
            if (lastSeen == null) return false;

            return lastSeen >= DateTime.UtcNow.AddMinutes(-2);
        }


        private static readonly string[] AllowedRoles =
{
    
    "Manager",
    "Accounting",
    "Hr"
};

        // ✅ إنشاء مستخدم جديد برقم تسلسلي صحيح
        public async Task<UserResponseDto> CreateAsync(CreateUserDto createDto)
        {
            var nextNumericId =
                (await _userManager.Users.MaxAsync(u => (int?)u.NumericId) ?? 0) + 1;

            var user = new AppUser
            {
                UserName = createDto.Email,
                Email = createDto.Email,
                DisplayName = createDto.DisplayName,
                PhoneNumber = createDto.PhoneNumber,
                IsActive = createDto.IsActive,
                EmailConfirmed = true,
                NumericId = nextNumericId
            };

            var result = await _userManager.CreateAsync(user, createDto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            // ✅ تحقق من الرول
            if (!string.IsNullOrWhiteSpace(createDto.Role))
            {
                if (!AllowedRoles.Contains(createDto.Role))
                    throw new Exception("Invalid role");

                await _userManager.AddToRoleAsync(user, createDto.Role);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var dto = _mapper.Map<UserResponseDto>(user);

            dto.Role = roles;
            dto.RoleName = roles.FirstOrDefault() ?? "";

            return dto;
        }


        // ✅ تحديث بيانات المستخدم برقم NumericId
        public async Task<bool> UpdateAsync(int numericId, UpdateUserDto dto)
        {
            var user = await GetUserByNumericIdAsync(numericId)
                ?? throw new Exception("User not found");

            if (dto.DisplayName != null)
                user.DisplayName = dto.DisplayName;

            if (dto.Email != null)
            {
                user.Email = dto.Email;
                user.UserName = dto.Email;
            }

            if (dto.PhoneNumber != null)
                user.PhoneNumber = dto.PhoneNumber;

            // ✅ تغيير الباسورد
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var reset = await _userManager.ResetPasswordAsync(user, token, dto.Password);

                if (!reset.Succeeded)
                    throw new Exception(string.Join(", ", reset.Errors.Select(e => e.Description)));
            }

            // ✅ تغيير الرول بأمان
            if (!string.IsNullOrWhiteSpace(dto.Role))
            {
                if (!AllowedRoles.Contains(dto.Role))
                    throw new Exception("Invalid role");

                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, dto.Role);
            }

            // ✅ partial update
            if (dto.IsActive.HasValue)
                user.IsActive = dto.IsActive.Value;

            var update = await _userManager.UpdateAsync(user);

            if (!update.Succeeded)
                throw new Exception(string.Join(", ", update.Errors.Select(e => e.Description)));

            return true;
        }


        public async Task<bool> DeleteAsync(int numericId)
        {
            var user = await GetUserByNumericIdAsync(numericId)
                ?? throw new Exception("User not found");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            return true;
        }


        // ♻️ إعادة ترقيم المستخدمين حسب الترتيب الزمني
        public async Task UpdateAllNumericIdsAsync()
        {
            var users = await _userManager.Users
                .OrderBy(u => u.Id)
                .ToListAsync();

            int counter = 1;
            foreach (var user in users)
            {
                user.NumericId = counter++;
                await _userManager.UpdateAsync(user);
            }
        }

        // 🔍 دالة مساعدة للبحث بالمفتاح NumericId
        private async Task<AppUser?> GetUserByNumericIdAsync(int numericId)
        {
            return await _userManager.Users
                .FirstOrDefaultAsync(u => u.NumericId == numericId);
        }

        public async Task<UserResponseDto> UpdateMyProfileAsync(
    string userId,
    UpdateMyProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new Exception("User not found");

            // الاسم
            if (!string.IsNullOrWhiteSpace(dto.DisplayName))
                user.DisplayName = dto.DisplayName;

            // الموبايل
            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
                user.PhoneNumber = dto.PhoneNumber;

            // =========================
            // تغيير الإيميل بأمان
            // =========================
            if (!string.IsNullOrWhiteSpace(dto.Email)
                && dto.Email.ToLower() != user.Email.ToLower())
            {
                var existingUser = await _userManager.FindByEmailAsync(dto.Email);

                if (existingUser != null)
                    throw new Exception("Email already in use");

                var token = await _userManager.GenerateChangeEmailTokenAsync(user, dto.Email);

                var result = await _userManager.ChangeEmailAsync(user, dto.Email, token);

                if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

                // لازم يتغير UserName كمان
                user.UserName = dto.Email;
            }

            var update = await _userManager.UpdateAsync(user);

            if (!update.Succeeded)
                throw new Exception(string.Join(", ", update.Errors.Select(e => e.Description)));

            var roles = await _userManager.GetRolesAsync(user);

            var response = _mapper.Map<UserResponseDto>(user);
            response.Role = roles;
            response.RoleName = roles.FirstOrDefault() ?? "";

            return response;
        }

    }
}
