using Shared.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.IUser
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> GetByIdAsync(int numericId);
        Task<UserResponseDto> CreateAsync(CreateUserDto createDto);
        Task UpdateAllNumericIdsAsync(); // ✅ الميثود الجديدة
        Task<bool> UpdateAsync(int numericId, UpdateUserDto updateDto);
        Task<UserResponseDto> UpdateMyProfileAsync(
    string userId,
    UpdateMyProfileDto dto);
        Task<bool> DeleteAsync(int numericId);
    }
}
