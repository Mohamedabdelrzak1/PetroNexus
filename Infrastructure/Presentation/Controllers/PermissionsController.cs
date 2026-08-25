using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.RolePermissions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PermissionsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public PermissionsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<PermissionResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.PermissionService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<PermissionResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.PermissionService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<PermissionResponseDto>($"Permission #{id} not found.");
            return this.OkResponse(result);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<PermissionResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.PermissionService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PermissionResponseDto>>> Create([FromBody] PermissionCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.PermissionService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] PermissionUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.PermissionService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.PermissionService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }
    }
}