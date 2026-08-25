using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.Clients;

namespace Presentation.Controllers
{
    /// <summary>
    /// Manages clients (customers) in the CRM module.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ClientsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ClientsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>Retrieves a paginated, searchable, sortable list of all clients.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<ClientResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PagedResult<ClientResponseDto>>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sector,
            [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.ClientService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Retrieves detailed information for a specific client by ID.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<ClientResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<ClientResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.ClientService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<ClientResponseDto>($"Client #{id} not found.");
            return this.OkResponse(result);
        }

        /// <summary>Returns a lightweight lookup list (id + name) for dropdowns.</summary>
        [HttpGet("lookup")]
        [ProducesResponseType(typeof(ApiResponse<System.Collections.Generic.IEnumerable<ClientResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<ClientResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.ClientService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Returns a summary list of clients.</summary>
        [HttpGet("summary")]
        [ProducesResponseType(typeof(ApiResponse<System.Collections.Generic.IEnumerable<ClientResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<ClientResponseDto>>>> Summary(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.ClientService.GetSummaryAsync(cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Creates a new client record.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ClientResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<ClientResponseDto>>> Create([FromBody] ClientCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.ClientService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Updates an existing client by ID.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] ClientUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.ClientService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        /// <summary>Deletes a client by ID.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.ClientService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }
    }
}