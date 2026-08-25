using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.Users;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UsersController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPut("profile")]
        [Authorize(Roles = "Manager,Hr,Accounting")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> UpdateMyProfile(UpdateMyProfileDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return this.UnauthorizedResponse<UserResponseDto>("User not authenticated.");

            var result = await _service.UserService.UpdateMyProfileAsync(userId, dto);

            return this.OkResponse(result, "Profile updated successfully.");
        }

        /// <summary>Get all users.</summary>
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<UserResponseDto>>>> GetAllUsers()
        {
            var users = await _service.UserService.GetAllAsync();
            return this.OkResponse(users);
        }

        [HttpPost("fix-numeric-ids")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<ApiResponse>> FixNumericIds()
        {
            await _service.UserService.UpdateAllNumericIdsAsync();
            return this.OkResponse("NumericIds updated successfully.");
        }

        /// <summary>Get user by Numeric ID.</summary>
        [HttpGet("{numericId:int}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetUserById(int numericId)
        {
            var user = await _service.UserService.GetByIdAsync(numericId);
            if (user == null)
                return this.NotFoundResponse<UserResponseDto>("User not found.");

            return this.OkResponse(user);
        }

        /// <summary>Create a new user.</summary>
        [HttpPost("create")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> CreateUser([FromBody] CreateUserDto createDto)
        {
            try
            {
                var user = await _service.UserService.CreateAsync(createDto);
                return this.CreatedResponse(nameof(GetUserById), new { numericId = user.NumericId }, user);
            }
            catch (System.InvalidOperationException ex)
            {
                return this.BadRequestResponse<UserResponseDto>(ex.Message);
            }
        }

        /// <summary>Update user by Numeric ID.</summary>
        [HttpPut("Update/{numericId:int}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<ApiResponse>> UpdateUser(int numericId, [FromBody] UpdateUserDto updateDto)
        {
            var result = await _service.UserService.UpdateAsync(numericId, updateDto);
            if (!result)
                return this.NotFoundResponse("User not found.");

            return this.OkResponse("User updated successfully.");
        }

        /// <summary>Delete user by Numeric ID.</summary>
        [HttpDelete("Delete/{numericId:int}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<ApiResponse>> DeleteUser(int numericId)
        {
            var result = await _service.UserService.DeleteAsync(numericId);
            if (!result)
                return this.NotFoundResponse("User not found.");

            return this.OkResponse("User deleted successfully.");
        }
    }
}