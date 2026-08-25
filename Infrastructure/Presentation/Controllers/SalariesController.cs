using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.Salaries;

namespace Presentation.Controllers
{
    /// <summary>
    /// Manages employee salary records with generate, approve, and mark-as-paid workflows.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class SalariesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SalariesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>Retrieves a paginated list of all salary records.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<SalaryResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PagedResult<SalaryResponseDto>>>> GetAll(
            [FromQuery] int? employeeId, [FromQuery] int? month, [FromQuery] int? year,
            [FromQuery] string? sortBy, [FromQuery] bool sortDesc = false,
            [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.SalaryService.GetAllAsync(null, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Retrieves a specific salary record by ID.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<SalaryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<SalaryResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.SalaryService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<SalaryResponseDto>($"Salary record #{id} not found.");
            return this.OkResponse(result);
        }

        /// <summary>Creates a manual salary record.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<SalaryResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<SalaryResponseDto>>> Create([FromBody] SalaryCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.SalaryService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Updates an existing salary record.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] SalaryUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.SalaryService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        /// <summary>Deletes a salary record by ID.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.SalaryService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }

        // ─── Business Endpoints ─────────────────────────────────────────────

        /// <summary>Auto-generates a salary record for an employee for the specified month/year.</summary>
        [HttpPost("generate")]
        [ProducesResponseType(typeof(ApiResponse<SalaryResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<SalaryResponseDto>>> Generate(
            [FromQuery] int employeeId, [FromQuery] int month,
            [FromQuery] int year, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.SalaryService.GenerateSalaryAsync(employeeId, month, year, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Approves a salary record (changes status from Draft to Approved).</summary>
        [HttpPost("{id:int}/approve")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Approve(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.SalaryService.ApproveAsync(id, cancellationToken);
            return this.OkResponse("Approved successfully.");
        }

        /// <summary>Marks a salary record as Paid.</summary>
        [HttpPost("{id:int}/mark-paid")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> MarkAsPaid(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.SalaryService.MarkAsPaidAsync(id, cancellationToken);
            return this.OkResponse("Marked as paid successfully.");
        }
    }
}