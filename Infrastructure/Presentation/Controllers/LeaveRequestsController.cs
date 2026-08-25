using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.LeaveRequests;

namespace Presentation.Controllers
{
    /// <summary>
    /// Manages employee leave requests with approve/reject workflow.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public LeaveRequestsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>Retrieves a paginated list of all leave requests.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<LeaveRequestResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PagedResult<LeaveRequestResponseDto>>>> GetAll(
            [FromQuery] int? employeeId, [FromQuery] bool? isApproved,
            [FromQuery] string? sortBy, [FromQuery] bool sortDesc = false,
            [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.LeaveRequestService.GetAllAsync(null, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Retrieves a specific leave request by ID.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<LeaveRequestResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<LeaveRequestResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.LeaveRequestService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<LeaveRequestResponseDto>($"Leave request #{id} not found.");
            return this.OkResponse(result);
        }

        /// <summary>Submits a new leave request.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<LeaveRequestResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<LeaveRequestResponseDto>>> Create([FromBody] LeaveRequestCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.LeaveRequestService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Updates an existing leave request.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] LeaveRequestUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.LeaveRequestService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        /// <summary>Deletes a leave request.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.LeaveRequestService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }

        // ─── Business Endpoints ─────────────────────────────────────────────

        /// <summary>Approves a pending leave request.</summary>
        [HttpPost("{id:int}/approve")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Approve(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.LeaveRequestService.ApproveAsync(id, cancellationToken);
            return this.OkResponse("Approved successfully.");
        }

        /// <summary>Rejects a pending leave request.</summary>
        [HttpPost("{id:int}/reject")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Reject(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.LeaveRequestService.RejectAsync(id, cancellationToken);
            return this.OkResponse("Rejected successfully.");
        }
    }
}