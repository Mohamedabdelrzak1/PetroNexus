using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Dto.Roles;

namespace Presentation.Controllers
{
    /// <summary>
    /// Manages application roles (CRUD + lookup).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>Lists all roles with descriptions.</summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<RoleResponseDto>>>> GetAll()
        {
            var roles = await _roleManager.Roles
                .OrderBy(r => r.Name)
                .Select(r => new RoleResponseDto
                {
                    Id = r.Id,
                    Name = r.Name ?? "",
                    Description = r.Description
                })
                .ToListAsync();

            return this.OkResponse(roles);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<ApiResponse<List<RoleLookupDto>>>> Lookup()
        {
            var roles = await _roleManager.Roles
                .OrderBy(r => r.Name)
                .Select(r => new RoleLookupDto
                {
                    Id = r.Id,
                    Name = r.Name ?? ""
                })
                .ToListAsync();

            return this.OkResponse(roles);
        }

        /// <summary>Creates a new role.</summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<RoleResponseDto>>> Create([FromBody] RoleCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return this.BadRequestResponse<RoleResponseDto>("Role name is required.");

            var existing = await _roleManager.FindByNameAsync(dto.Name);
            if (existing != null)
                return this.ConflictResponse<RoleResponseDto>($"Role '{dto.Name}' already exists.");

            var role = new AppRole
            {
                Name = dto.Name,
                Description = dto.Description
            };

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                return this.BadRequestResponse<RoleResponseDto>(
                    "Failed to create role.",
                    result.Errors.Select(e => e.Description).ToList());

            return this.OkResponse(new RoleResponseDto
            {
                Id = role.Id,
                Name = role.Name ?? "",
                Description = role.Description
            }, "Role created successfully.");
        }

        /// <summary>Updates an existing role.</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> Update(string id, [FromBody] RoleUpdateDto dto)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return this.NotFoundResponse($"Role #{id} not found.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return this.BadRequestResponse("Role name is required.");

            role.Name = dto.Name;
            role.Description = dto.Description;

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return this.BadRequestResponse(
                    "Failed to update role.",
                    result.Errors.Select(e => e.Description).ToList());

            return this.OkResponse("Role updated successfully.");
        }

        /// <summary>Deletes a role. Prevents deletion if users are assigned to it.</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return this.NotFoundResponse($"Role #{id} not found.");

            // Prevent deletion if any users are assigned to this role
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name ?? "");
            if (usersInRole.Any())
                return this.ConflictResponse("لا يمكن حذف دور مرتبط بمستخدمين نشطين");

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return this.BadRequestResponse(
                    "Failed to delete role.",
                    result.Errors.Select(e => e.Description).ToList());

            return this.OkResponse("Role deleted successfully.");
        }
    }
}