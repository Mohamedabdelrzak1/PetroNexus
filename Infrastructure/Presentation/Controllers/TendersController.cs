using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.Tenders;

namespace Presentation.Controllers
{
    /// <summary>
    /// Manages tender records in the CRM/Tenders module.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TendersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TendersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>Retrieves a paginated list of all tenders with optional search and sort.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<TenderResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PagedResult<TenderResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Retrieves detailed information for a tender by ID.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<TenderResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<TenderResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<TenderResponseDto>($"Tender #{id} not found.");
            return this.OkResponse(result);
        }

        /// <summary>Returns a lookup list of tenders for dropdowns.</summary>
        [HttpGet("lookup")]
        [ProducesResponseType(typeof(ApiResponse<System.Collections.Generic.IEnumerable<TenderResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<TenderResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Creates a new tender record.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TenderResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<TenderResponseDto>>> Create([FromBody] TenderCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.TenderService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Updates an existing tender by ID.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] TenderUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.TenderService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        /// <summary>Deletes a tender by ID.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.TenderService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }
    }
}