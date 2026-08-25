using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.PurchaseOrders;

namespace Presentation.Controllers
{
    /// <summary>
    /// Manages purchase orders to principals.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public PurchaseOrdersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<PurchaseOrderResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<PagedResult<PurchaseOrderResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.PurchaseOrderService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<PurchaseOrderResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<PurchaseOrderResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.PurchaseOrderService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<PurchaseOrderResponseDto>($"PurchaseOrder #{id} not found.");
            return this.OkResponse(result);
        }

        [HttpGet("lookup")]
        [ProducesResponseType(typeof(ApiResponse<System.Collections.Generic.IEnumerable<PurchaseOrderResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<PurchaseOrderResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.PurchaseOrderService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("summary")]
        [ProducesResponseType(typeof(ApiResponse<System.Collections.Generic.IEnumerable<PurchaseOrderResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<PurchaseOrderResponseDto>>>> Summary(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.PurchaseOrderService.GetSummaryAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<PurchaseOrderResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<PurchaseOrderResponseDto>>> Create([FromBody] PurchaseOrderCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.PurchaseOrderService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] PurchaseOrderUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.PurchaseOrderService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.PurchaseOrderService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }
    }
}