using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.Invoices;

namespace Presentation.Controllers
{
    /// <summary>
    /// Manages invoices issued to clients.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class InvoicesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public InvoicesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<InvoiceResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<PagedResult<InvoiceResponseDto>>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.InvoiceService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<InvoiceResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.InvoiceService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<InvoiceResponseDto>($"Invoice #{id} not found.");
            return this.OkResponse(result);
        }

        [HttpGet("lookup")]
        [ProducesResponseType(typeof(ApiResponse<System.Collections.Generic.IEnumerable<InvoiceResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<InvoiceResponseDto>>>> Lookup(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.InvoiceService.GetLookupAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("summary")]
        [ProducesResponseType(typeof(ApiResponse<System.Collections.Generic.IEnumerable<InvoiceResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<System.Collections.Generic.IEnumerable<InvoiceResponseDto>>>> Summary(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.InvoiceService.GetSummaryAsync(cancellationToken);
            return this.OkResponse(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<InvoiceResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> Create([FromBody] InvoiceCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.InvoiceService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] InvoiceUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.InvoiceService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.InvoiceService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }

        /// <summary>Approves an invoice and posts its journal entry.</summary>
        [HttpPost("{id:int}/approve")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Approve(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.InvoiceService.ApproveAsync(id, cancellationToken);
            return this.OkResponse("Invoice approved successfully.");
        }

        /// <summary>Cancels an invoice.</summary>
        [HttpPost("{id:int}/cancel")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Cancel(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.InvoiceService.CancelAsync(id, cancellationToken);
            return this.OkResponse("Invoice cancelled.");
        }

        /// <summary>Records a payment against an invoice.</summary>
        [HttpPost("{id:int}/record-payment")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> RecordPayment(int id, [FromBody] InvoicePaymentRequest request, CancellationToken cancellationToken = default)
        {
            await _serviceManager.InvoiceService.RecordPaymentAsync(id, request.Amount, request.Method, request.ReferenceNumber, cancellationToken);
            return this.OkResponse("Payment recorded successfully.");
        }

        /// <summary>Generates a new invoice from a tender.</summary>
        [HttpPost("generate/{tenderId:int}")]
        [ProducesResponseType(typeof(ApiResponse<InvoiceResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> GenerateFromTender(int tenderId, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.InvoiceService.GenerateInvoiceAsync(tenderId, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }
    }

    /// <summary>Request model for recording a payment against an invoice.</summary>
    public class InvoicePaymentRequest
    {
        public decimal Amount { get; set; }
        public string Method { get; set; } = null!;
        public string ReferenceNumber { get; set; } = null!;
    }
}