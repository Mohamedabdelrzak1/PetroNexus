using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.Attendances;

namespace Presentation.Controllers
{
    /// <summary>
    /// Manages employee attendance records including check-in, check-out, fingerprint import, and late calculation.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AttendancesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AttendancesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>Retrieves a paginated list of attendance records.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<AttendanceResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PagedResult<AttendanceResponseDto>>>> GetAll(
            [FromQuery] int? employeeId, [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.AttendanceService.GetAllAsync(null, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Retrieves a specific attendance record by ID.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<AttendanceResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<AttendanceResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.AttendanceService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<AttendanceResponseDto>($"Attendance record #{id} not found.");
            return this.OkResponse(result);
        }

        /// <summary>Creates a manual attendance record.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AttendanceResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<AttendanceResponseDto>>> Create([FromBody] AttendanceCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.AttendanceService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Updates an attendance record.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] AttendanceUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.AttendanceService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        /// <summary>Deletes an attendance record by ID.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.AttendanceService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }

        // ─── Business Endpoints ─────────────────────────────────────────────

        /// <summary>Records a check-in for an employee at the current UTC time.</summary>
        [HttpPost("checkin")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CheckIn([FromQuery] int employeeId, CancellationToken cancellationToken = default)
        {
            await _serviceManager.AttendanceService.CheckInAsync(employeeId, cancellationToken);
            return this.OkResponse("Check-in recorded successfully.");
        }

        /// <summary>Records a check-out for an employee at the current UTC time.</summary>
        [HttpPost("checkout")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CheckOut([FromQuery] int employeeId, CancellationToken cancellationToken = default)
        {
            await _serviceManager.AttendanceService.CheckOutAsync(employeeId, cancellationToken);
            return this.OkResponse("Check-out recorded successfully.");
        }

        /// <summary>Imports attendance records from a connected fingerprint device.</summary>
        [HttpPost("import-fingerprint")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> ImportFingerprint(CancellationToken cancellationToken = default)
        {
            await _serviceManager.AttendanceService.ImportFingerprintAsync(cancellationToken);
            return this.OkResponse("Fingerprint data imported successfully.");
        }

        /// <summary>Calculates and persists late minutes for an employee on a given date.</summary>
        [HttpPost("calculate-late")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<int>>> CalculateLate([FromQuery] int employeeId, [FromQuery] DateTime date, CancellationToken cancellationToken = default)
        {
            var minutes = await _serviceManager.AttendanceService.CalculateLateAsync(employeeId, date, cancellationToken);
            return this.OkResponse(minutes, "Late minutes calculated successfully.");
        }
    }
}