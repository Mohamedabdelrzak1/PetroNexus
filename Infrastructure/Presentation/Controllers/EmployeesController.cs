using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.Employees;

namespace Presentation.Controllers
{
    /// <summary>
    /// Manages employee records and HR business operations (transfer, activate, terminate).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>Retrieves a paginated, filterable list of employees.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<EmployeeResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PagedResult<EmployeeResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] int? departmentId,
            [FromQuery] bool? isActive, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EmployeeService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Retrieves an employee by ID.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<EmployeeResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<EmployeeResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EmployeeService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<EmployeeResponseDto>($"Employee #{id} not found.");
            return this.OkResponse(result);
        }

        /// <summary>Returns a lookup list of employees (id + name) for dropdowns.</summary>
        [HttpGet("lookup")]
        [ProducesResponseType(typeof(ApiResponse<System.Collections.Generic.IEnumerable<EmployeeResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<EmployeeResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EmployeeService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Creates a new employee record.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<EmployeeResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<EmployeeResponseDto>>> Create([FromBody] EmployeeCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.EmployeeService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Updates an existing employee record.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] EmployeeUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.EmployeeService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        /// <summary>Deletes an employee record.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.EmployeeService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }

        // ─── Business Endpoints ─────────────────────────────────────────────

        /// <summary>Transfers an employee to a different department.</summary>
        [HttpPost("{id:int}/transfer")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Transfer(int id, [FromQuery] int newDepartmentId, CancellationToken cancellationToken = default)
        {
            await _serviceManager.EmployeeService.TransferDepartmentAsync(id, newDepartmentId, cancellationToken);
            return this.OkResponse("Transferred successfully.");
        }

        /// <summary>Terminates (deactivates) an employee.</summary>
        [HttpPost("{id:int}/terminate")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Terminate(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.EmployeeService.TerminateAsync(id, cancellationToken);
            return this.OkResponse("Terminated successfully.");
        }

        /// <summary>Activates a previously terminated employee.</summary>
        [HttpPost("{id:int}/activate")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Activate(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.EmployeeService.ActivateAsync(id, cancellationToken);
            return this.OkResponse("Activated successfully.");
        }
    }
}