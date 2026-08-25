using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.DocumentSignatures;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class DocumentSignaturesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public DocumentSignaturesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<DocumentSignatureResponseDto>>>> GetAll(
            [FromQuery] string? search, [FromQuery] string? sortBy,
            [FromQuery] bool sortDesc = false, [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DocumentSignatureService.GetAllAsync(search, pageIndex, pageSize, sortBy, sortDesc, cancellationToken);
            return this.OkResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<DocumentSignatureResponseDto>>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DocumentSignatureService.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return this.NotFoundResponse<DocumentSignatureResponseDto>($"DocumentSignature #{id} not found.");
            return this.OkResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<DocumentSignatureResponseDto>>> Create([FromBody] DocumentSignatureCreateDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DocumentSignatureService.CreateAsync(dto, cancellationToken);
            return this.CreatedResponse(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] DocumentSignatureUpdateDto dto, CancellationToken cancellationToken = default)
        {
            await _serviceManager.DocumentSignatureService.UpdateAsync(id, dto, cancellationToken);
            return this.OkResponse("Updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.DocumentSignatureService.DeleteAsync(id, cancellationToken);
            return this.OkResponse("Deleted successfully.");
        }

        /// <summary>Signs a document signature (computes SHA256 hash).</summary>
        [HttpPost("{id:int}/sign")]
        public async Task<ActionResult<ApiResponse>> Sign(int id, CancellationToken cancellationToken = default)
        {
            await _serviceManager.DocumentSignatureService.SignAsync(id, cancellationToken);
            return this.OkResponse("Document signed successfully.");
        }

        /// <summary>Rejects a document signature with a reason.</summary>
        [HttpPost("{id:int}/reject")]
        public async Task<ActionResult<ApiResponse>> Reject(int id, [FromBody] RejectSignatureRequest request, CancellationToken cancellationToken = default)
        {
            await _serviceManager.DocumentSignatureService.RejectAsync(id, request.RejectionReason, cancellationToken);
            return this.OkResponse("Document signature rejected.");
        }
    }

    /// <summary>Request model for rejecting a document signature.</summary>
    public class RejectSignatureRequest
    {
        public string RejectionReason { get; set; } = null!;
    }
}
